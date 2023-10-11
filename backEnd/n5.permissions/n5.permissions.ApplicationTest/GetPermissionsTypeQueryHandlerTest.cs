using Microsoft.Extensions.Configuration;
using Moq;
using n5.permissions.Application.Handlers;
using n5.permissions.Application.Query;
using n5.permissions.Domain.Entity;
using n5.permissions.Infraestructure.Kafka;
using n5.permissions.Infraestructure.UnitOfWorks;
using n5.permissions.Infraestructure.Utils;

namespace n5.permissions.UnitTest
{
    public class GetPermissionsTypeQueryHandlerTest
    {
        [Fact]
        public async Task HandleAsync_ReturnsPermissionTypeDtoList()
        {


            // Arrange
            var permissions = new List<PermissionType>
            {
                new PermissionType
                {
                    Id = 1,
              Descripcion = "Tipo 1"
                },
                new PermissionType
                {
                    Id = 2,
              Descripcion = "Tipo 2"
                }
            }.AsQueryable();

            IQueryable<PermissionType> queryableList = permissions.AsQueryable();

            var unitOfWorkMock = new Mock<IUnitOfWork<PermissionType, int>>();
            unitOfWorkMock.Setup(uow => uow.Repository.GetAll()).Returns(queryableList);

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(config => config["kafka:Topic"]).Returns("test-topic");

            var permissionsProducerMock = new Mock<IPermissionsProducer>();
            permissionsProducerMock.Setup(producer => producer.ProduceAsync("test-topic", EventType.Get.ToString())).Returns(Task.CompletedTask);

            var queryHandler = new GetPermissionsTypeQueryHandler(unitOfWorkMock.Object, configurationMock.Object, permissionsProducerMock.Object);

            var query = new GetPermissionsQuery();
            var result = await queryHandler.HandleAsync(query);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(2, result[1].Id);
            Assert.Equal("Tipo 2", result[1].Descripcion);
        }
    }
}
