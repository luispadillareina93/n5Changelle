using AutoMapper;
using Microsoft.Extensions.Configuration;
using Moq;
using n5.permissions.Application.Commands;
using n5.permissions.Application.Handlers;
using n5.permissions.Domain.Entity;
using n5.permissions.Infraestructure.Kafka;
using n5.permissions.Infraestructure.UnitOfWorks;
using Nest;
using Xunit;

namespace n5.permissions.Test.UnitTest
{
    public class RequestPermissionsHandlerTests
    {
        
        [Fact]
        public async Task HandleAsync_InsertsPermissionAndRegistersEvent()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork<Permission, int>>();
            var mapperMock = new Mock<IMapper>();
            var elasticClientMock = new Mock<IElasticClient>();
            var configurationMock = new Mock<IConfiguration>();
            var permissionsProducerMock = new Mock<IPermissionsProducer>();

            var handler = new RequestPermissionCommandHandler(
                unitOfWorkMock.Object,
                mapperMock.Object,
                elasticClientMock.Object,
                  permissionsProducerMock.Object,
                configurationMock.Object
              );

            var command = new RequestPermissionCommand();

            // Act
            await handler.HandleAsync(command);

            // Assert
            unitOfWorkMock.Verify(u => u.Repository.Insert(It.IsAny<Permission>()), Times.Once);
            unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
            permissionsProducerMock.Verify(p => p.ProduceAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

    }
}
