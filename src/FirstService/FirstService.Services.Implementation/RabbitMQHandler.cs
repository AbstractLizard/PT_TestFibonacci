namespace FirstService.Services.Implementation
{
    using System.Collections.Concurrent;
    using System.Threading;
    using EasyNetQ;
    using Message;

    /// <summary>
    /// Реализация <inheritdoc cref="IMQHandler"/> для RabbitMq
    /// </summary>
    public class RabbitMQHandler : IMQHandler
    {
        private readonly IBus _bus;

        private readonly IConfigProvider _configProvider;

        private BlockingCollection<MessageDto> _blockingCollection = null;
        private CancellationTokenSource _src;
        private CancellationToken _ct;
        
        public RabbitMQHandler(IBus bus, IConfigProvider configProvider)
        {
            _bus = bus;
            _configProvider = configProvider;
        }

        public void Start()
        {
            if (_blockingCollection == null)
            {
                _blockingCollection = new BlockingCollection<MessageDto>();
            }
            _src = new CancellationTokenSource();
            _ct = _src.Token;

            _bus.PubSub.SubscribeAsync<MessageDto>(_configProvider.RabbitMQSubscriptionId, HandleMqEvent, _ct);
        }
        public void Stop()
        {
            _blockingCollection.CompleteAdding();
            _ct.ThrowIfCancellationRequested();
            _blockingCollection = null;
        }
        
        public BlockingCollection<MessageDto> GetBlockingCollection()
        {
            return _blockingCollection;
        }

        private void HandleMqEvent(MessageDto dto)
        {
            if (_blockingCollection is {IsCompleted: false})
            {
                _blockingCollection.Add(dto);
            }
        }
    }
}