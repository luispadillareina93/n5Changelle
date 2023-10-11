using Confluent.Kafka;
using Newtonsoft.Json;

namespace n5.permissions.Infraestructure.Kafka
{
    public class PermissionsProducer : IPermissionsProducer
    {
        private readonly IProducer<Null, string> _producer;

        public PermissionsProducer(string bootstrapServers)
        {
            var config = new ProducerConfig { BootstrapServers = bootstrapServers };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task ProduceAsync(string topic, string operation)
        {
            var kafkaEvent = new PermissionEventDto
            {
                Id = Guid.NewGuid(),
                Operation = operation
            };

            var messageValue = JsonConvert.SerializeObject(kafkaEvent);

            try
            {
                var deliveryResult = await _producer.ProduceAsync(topic, new Message<Null, string> { Value = messageValue });

                Console.WriteLine($"Mensaje enviado a {deliveryResult.Topic}, Partición: {deliveryResult.Partition}, Offset: {deliveryResult.Offset}");
            }
            catch (ProduceException<Null, string> e)
            {
                Console.WriteLine($"Error al enviar mensaje: {e.Error.Reason}");
            }
        }

        public void Dispose()
        {
            _producer.Dispose();
        }


    }
}
