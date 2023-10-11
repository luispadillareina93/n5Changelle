using Microsoft.EntityFrameworkCore;
using n5.permissions.Application.Commands;
using n5.permissions.Application.Contracts;
using n5.permissions.Application.Dto;
using n5.permissions.Application.Handlers;
using n5.permissions.Application.Query;
using n5.permissions.Infraestructure.Contexts;
using n5.permissions.Infraestructure.Kafka;
using n5.permissions.Infraestructure.Repositories;
using n5.permissions.Infraestructure.UnitOfWorks;
using n5.permissions.Infraestructure.Utils;
using Nest;

var builder = WebApplication.CreateBuilder(args);

//ElasticSearchConfig
var elasticEndPoint = builder.Configuration.GetValue<string>("ElasticSearch:EndPoint");
var index = builder.Configuration.GetValue<string>("ElasticSearch:Index");

var connectionSettings = new ConnectionSettings(new Uri(elasticEndPoint)).DefaultIndex(index);
var elasticClient = new ElasticClient(connectionSettings);


// Add services to the container.
builder.Services.AddScoped(typeof(IUnitOfWork<,>), typeof(UnitOfWork<,>));
builder.Services.AddSingleton<IPermissionsProducer, PermissionsProducer>(provider =>
{
    var kafkaConfig = builder.Configuration.GetSection("Kafka").Get<KafkaConfiguration>();
    return new PermissionsProducer(kafkaConfig.BootstrapServers);
});

builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
builder.Services.AddScoped<ICommandHandler<RequestPermissionCommand>, RequestPermissionCommandHandler>();
builder.Services.AddScoped<ICommandHandler<ModifyPermissionCommand>, ModifyPermissionCommandHandler>();
builder.Services.AddScoped<IQueryHandler<GetPermissionsQuery, List<PermissionDto>>, GetPermissionsQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetPermissionsQuery, List<PermissionTypeDto>>, GetPermissionsTypeQueryHandler>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<KafkaConfiguration>(builder.Configuration.GetSection("Kafka"));


builder.Services.AddSingleton<IElasticClient>(elasticClient);
builder.Services.AddDbContext<PermissionsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsApi",
        builder => builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});
var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PermissionsDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsApi");

app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }
