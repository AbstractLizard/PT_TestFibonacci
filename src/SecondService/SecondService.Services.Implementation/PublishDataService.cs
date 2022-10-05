namespace SecondService.Services.Implementation
{
    using System.Threading.Tasks;
    using Common.Interface;
    using EasyNetQ;

    ///<inheritdoc cref="IPublishDataService"/>
    public class PublishDataService : IPublishDataService
    {
        private readonly IBus _bus;
        
        public PublishDataService(IBus bus)
        {
            _bus = bus;
        }

        public Task Publish<T>(T obj)
        {
           return _bus.PubSub.PublishAsync<T>(obj);
        }
    }
}