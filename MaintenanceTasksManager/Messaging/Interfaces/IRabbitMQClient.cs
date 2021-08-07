namespace Messaging.Interfaces
{
    public interface IRabbitMQClient
    {
        public void PushMessage(string routingKey, object message);
    }
}