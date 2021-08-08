namespace ApplicationService.Messaging
{
    using global::Messaging;
    using Microsoft.Extensions.Configuration;

    public class NewTaskNotificationListener : RabbitMQListener
    {
        public NewTaskNotificationListener(IConfiguration configuration) : base(
            configuration)
        {
            base.RouteKey = "task.created";
            base.QueueName = "messages";
        }
    }
}