using Dasync.Collections;
using Microsoft.Extensions.Configuration;
using Moq;
using n5.permissions.Application.Handlers;
using n5.permissions.Application.Query;
using n5.permissions.Domain.Entity;
using n5.permissions.Infraestructure.Kafka;
using n5.permissions.Infraestructure.UnitOfWorks;
using n5.permissions.Infraestructure.Utils;
using System.Xml;

namespace n5.permissions.UnitTest
{

    public class GetPermissionsQueryHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ReturnsPermissionDtoList()
        {


            // Arrange
            var permissions = new List<Permission>
            {
                new Permission
                {
                    Id = 1,
                    ApellidoEmpleado = "Reina",
                    NombreEmpleado = "Alexander",
                    FechaPermiso = DateTime.Now,
                    TipoPermisoId = 1,
                    TipoPermiso = new PermissionType { Descripcion = "Tipo 1" }
                },
                new Permission
                {
                    Id = 2,
                    ApellidoEmpleado = "Padilla",
                    NombreEmpleado = "Luis",
                    FechaPermiso = DateTime.Now,
                    TipoPermisoId = 2,
                    TipoPermiso = new PermissionType { Descripcion = "Tipo 2" }
                }
            }.AsQueryable();

            IQueryable<Permission> queryableList = permissions.AsQueryable();

            var unitOfWorkMock = new Mock<IUnitOfWork<Permission, int>>();
            unitOfWorkMock.Setup(uow => uow.Repository.GetAll()).Returns(queryableList);

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(config => config["kafka:Topic"]).Returns("test-topic");

            var permissionsProducerMock = new Mock<IPermissionsProducer>();
            permissionsProducerMock.Setup(producer => producer.ProduceAsync("test-topic", EventType.Get.ToString())).Returns(Task.CompletedTask);

            var queryHandler = new GetPermissionsQueryHandler(unitOfWorkMock.Object, configurationMock.Object, permissionsProducerMock.Object);

            var query = new GetPermissionsQuery();
            var result = await queryHandler.HandleAsync(query);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Reina", result[0].ApellidoEmpleado);
            Assert.Equal("Padilla", result[1].ApellidoEmpleado);
        }
    }


}
