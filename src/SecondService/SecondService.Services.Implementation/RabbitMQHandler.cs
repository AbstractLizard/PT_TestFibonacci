namespace SecondService.Services.Implementation
{
    using EasyNetQ;

    /// <summary>
    /// Реализация <inheritdoc cref="IMQHandler"/> для RabbitMq
    /// </summary>
    public class RabbitMQHandler<T> : IMQHandler<T> 
        where T : class 
    {
        private readonly IBus _bus;
        
        public RabbitMQHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Publish(T msg)
        {
           _bus.PubSub.PublishAsync<T>(msg).ConfigureAwait(false);;
        }
    }
}