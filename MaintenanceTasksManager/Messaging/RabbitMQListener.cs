namespace Messaging
{
    using System;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    public class RabbitMQListener : IHostedService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQListener(IConfiguration configuration)
        {
            try
            {
                var rabbitMQConfig = configuration.GetSection("RabbitMQ");
                
                var factory = new ConnectionFactory()
                {
                    HostName = rabbitMQConfig["Host"],
                    UserName = rabbitMQConfig["Username"],
                    Password = rabbitMQConfig["Password"],
                    Port = int.TryParse(rabbitMQConfig["Port"], out int port) ? port : 5672
                };

                this._connection = factory.CreateConnection();
                this._channel = _connection.CreateModel();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RabbitListener init error,ex:{ex.Message}");
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Register();
            return Task.CompletedTask;
        }

        protected string RouteKey;
        protected string QueueName;

        public bool Process(string message)
        {
            Console.WriteLine(message);

            return string.IsNullOrWhiteSpace(message);
        }

        // Registered consumer monitoring here
        public void Register()
        {
            Console.WriteLine($"RabbitMQListener register,routeKey:{RouteKey}");

            _channel.ExchangeDeclare(exchange: "message", type: "topic");
            _channel.QueueDeclare(queue: QueueName, exclusive: false);
            _channel.QueueBind(queue: QueueName,
                exchange: "message",
                routingKey: RouteKey);

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.Span);
                var result = Process(message);
                if (result)
                {
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
            };
            _channel.BasicConsume(queue: QueueName, consumer: consumer);
        }

        public void DeRegister()
        {
            this._connection.Close();
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            this._connection.Close();
            return Task.CompletedTask;
        }
    }
}