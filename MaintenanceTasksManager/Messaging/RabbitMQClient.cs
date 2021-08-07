namespace Messaging
{
    using System;
    using System.Text;
    using Messaging.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    public class RabbitMQClient : IRabbitMQClient
    {
        private readonly IModel _channel;

        public RabbitMQClient(IConfiguration configuration)
        {
            var rabbitMQConfig = configuration.GetSection("RabbitMQ");

            var factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfig["Host"],
                UserName = rabbitMQConfig["Username"],
                Password = rabbitMQConfig["Password"],
                Port = int.TryParse(rabbitMQConfig["Port"], out int port) ? port : 5672
            };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
        }

        public virtual void PushMessage(string routingKey, object message)
        {
            _channel.QueueDeclare(queue: "message",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            string msgJson = JsonConvert.SerializeObject(message);

            var body = Encoding.UTF8.GetBytes(msgJson);
            _channel.BasicPublish(exchange: "message",
                routingKey: routingKey,
                basicProperties: null,
                body: body);
        }

        public void Subscribe()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
            };
            _channel.BasicConsume(queue: "message",
                autoAck: true,
                consumer: consumer);
        }
    }
}