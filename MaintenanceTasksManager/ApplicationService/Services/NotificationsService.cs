namespace ApplicationService.Services
{
    using ApplicationService.Services.Interfaces;
    using Messaging.Interfaces;

    public class NotificationsService : INotificationsService
    {
        private const string RoutingKey = "task.created";
        
        private readonly IRabbitMQClient _mqClient;
        
        public NotificationsService(IRabbitMQClient mqClient)
        {
            _mqClient = mqClient;
        }
        
        public void SendNotification(string notification)
        {
            _mqClient.PushMessage(RoutingKey, notification);
        }
    }
}