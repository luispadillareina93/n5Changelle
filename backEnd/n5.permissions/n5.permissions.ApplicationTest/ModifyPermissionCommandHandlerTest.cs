using AutoMapper;
using Microsoft.Extensions.Configuration;
using Moq;
using n5.permissions.Application.Commands;
using n5.permissions.Application.Handlers;
using n5.permissions.Application.Mappers;
using n5.permissions.Domain.Entity;
using n5.permissions.Infraestructure.Kafka;
using n5.permissions.Infraestructure.UnitOfWorks;
using Nest;

namespace n5.permissions.UnitTest
{
    public class ModifyPermissionCommandHandlerTest
    {
        private readonly IMapper _mapperMock;
        private UnitOfWork<Permission, int> _unitOfWorkMock;
        public ModifyPermissionCommandHandlerTest()
        {

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingPermission>();

            });
            _mapperMock = configuration.CreateMapper();
        }

        [Fact]
        public async Task HandleAsync_UpdatePermissionAndRegistersEvent()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork<Permission, int>>();
            var elasticClientMock = new Mock<IElasticClient>();
            var configurationMock = new Mock<IConfiguration>();
            var permissionsProducerMock = new Mock<IPermissionsProducer>();

            var handler = new ModifyPermissionCommandHandler(
                unitOfWorkMock.Object,
                _mapperMock,
                elasticClientMock.Object,
                permissionsProducerMock.Object,
                configurationMock.Object
                );

            var command = new ModifyPermissionCommand()
            {
                ApellidoEmpleado = "Luis Test",
                NombreEmpleado = "Padilla Test",
                TipoPermisoId = 1,
                FechaPermiso = DateTime.Now


            };
            var entity = _mapperMock.Map<Permission>(command);
            unitOfWorkMock.Setup(repo => repo.Repository.Update(entity)).Returns(
                 entity
                );


            // Act
            await handler.HandleAsync(command);

            // Assert
            unitOfWorkMock.Verify(u => u.Repository.Update(It.IsAny<Permission>()), Times.Once);
            unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
            permissionsProducerMock.Verify(p => p.ProduceAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
