using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using n5.permissions.Application.Commands;
using n5.permissions.Application.Dto;
using n5.permissions.Infraestructure.Contexts;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;


namespace n5.permissions.IntegrationTest
{
    public class PermissionIntegrationTest
    {

        private readonly string _baseUrl = "https://localhost:44340";

        private readonly HttpClient _httpClient;
        public PermissionIntegrationTest()
        {
            var configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory()) 
                                .AddJsonFile("appsettings.json")      
                                .Build();

            var conectionString = configuration["ConnectionStrings:Default"];

            var app = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                               d => d.ServiceType ==
                                   typeof(DbContextOptions<PermissionsDbContext>));
                    services.Remove(descriptor);
                    services.AddDbContext<PermissionsDbContext>(options =>
                          options.UseSqlServer(conectionString)
                         );

                });

            });

            _httpClient = app.CreateClient(new() { BaseAddress = new Uri(_baseUrl) });

        }
        [Fact]
        public async Task Verify_RequestPermission_Success()
        {
            // Arrange

            const string requestEndPoint = "api/Permission/RequestPermissions";
            const string getEndPoint = "api/Permission/GetPermissions";

            var date = DateTime.Now;

            var input = new RequestPermissionCommand()
            {
                ApellidoEmpleado = "Luis Test Integration",
                NombreEmpleado = "Padilla Test Integration",
                TipoPermisoId = 1,
                FechaPermiso = date
            };
            var body = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
            body.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            var responseRequestPermission = await _httpClient.PostAsync(requestEndPoint, body);

            var contentRequestPermission = await responseRequestPermission.Content.ReadAsStringAsync();

            var resultRequestPermission = System.Text.Json.JsonSerializer.Deserialize<RequestPermissionCommand>(contentRequestPermission);

            // Assert
            responseRequestPermission.EnsureSuccessStatusCode();
            Assert.NotNull(resultRequestPermission);


            var responseGetPermission = await _httpClient.GetAsync(getEndPoint);
            var contentGetPermission = await responseGetPermission.Content.ReadAsStringAsync();
            var resultGetPermission = JsonConvert.DeserializeObject<List<PermissionDto>>(contentGetPermission);
            var newPermission = resultGetPermission?.FirstOrDefault(c => c.NombreEmpleado == input.NombreEmpleado && c.ApellidoEmpleado == input.ApellidoEmpleado && c.TipoPermisoId == input.TipoPermisoId && input.FechaPermiso == date);

            responseGetPermission.EnsureSuccessStatusCode();
            Assert.NotNull(newPermission);
        }

        [Fact]
        public async Task Verify_RequestPermissionAndUpdate_Success()
        {
            // Arrange

            const string requestEndPoint = "api/Permission/RequestPermissions";
            const string modifyEndPoint = "api/Permission/ModifyPermissions";
            const string getEndPoint = "api/Permission/GetPermissions";

            var date = DateTime.Now;

            var input = new RequestPermissionCommand()
            {
                ApellidoEmpleado = "Luis Test Integration",
                NombreEmpleado = "Padilla Test Integration",
                TipoPermisoId = 1,
                FechaPermiso = date
            };
            var body = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
            body.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            var responseRequestPermission = await _httpClient.PostAsync(requestEndPoint, body);

            var contentRequestPermission = await responseRequestPermission.Content.ReadAsStringAsync();

            var resultRequestPermission = JsonConvert.DeserializeObject<PermissionDto>(contentRequestPermission);

            // Assert
            responseRequestPermission.EnsureSuccessStatusCode();
            Assert.NotNull(resultRequestPermission);


            var responseGetPermission = await _httpClient.GetAsync(getEndPoint);
            var contentGetPermission = await responseGetPermission.Content.ReadAsStringAsync();
            var resultGetPermission = JsonConvert.DeserializeObject<List<PermissionDto>>(contentGetPermission);
            var newPermission = resultGetPermission?.FirstOrDefault(c => c.NombreEmpleado == input.NombreEmpleado && c.ApellidoEmpleado == input.ApellidoEmpleado && c.TipoPermisoId == input.TipoPermisoId && input.FechaPermiso == date);

            responseGetPermission.EnsureSuccessStatusCode();
            Assert.NotNull(newPermission);
            const string updateNameEmpleado = "Update Name Empleado";


            newPermission.NombreEmpleado = updateNameEmpleado;

            var bodyUpdate = new StringContent(JsonConvert.SerializeObject(newPermission), Encoding.UTF8, "application/json");
            bodyUpdate.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var responseModifyPermission = await _httpClient.PutAsync(modifyEndPoint, bodyUpdate);

            var contentModifyPermission = await responseModifyPermission.Content.ReadAsStringAsync();
            var resultModifyPermission = JsonConvert.DeserializeObject<PermissionDto>(contentModifyPermission);

            responseModifyPermission.EnsureSuccessStatusCode();
            Assert.NotNull(resultModifyPermission);
            Assert.Equal(updateNameEmpleado, resultModifyPermission.NombreEmpleado);
        }
    }
}
