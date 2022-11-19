namespace Jobsity.Chat.Services.RabbitMQ
{
    using global::RabbitMQ.Client;
    using Jobsity.Chat.Borders.Configuration;
    using Jobsity.Chat.Borders.Dto;
    using Newtonsoft.Json;
    using System.Text;

    public class Producer : IProducer
    {
        private readonly RabbitMq _rabbitMq;

        public Producer(ApplicationConfig applicationConfig)
        {
            _rabbitMq = applicationConfig.RabbitMq!;
        }

        public async Task Send(MessageDto messageDto)
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMq.Hostname,
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: _rabbitMq.QueueName, durable: true, exclusive: false, autoDelete: false);

            var sendBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageDto));
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(string.Empty, _rabbitMq.QueueName, true, properties, sendBytes);
        }
    }
}
