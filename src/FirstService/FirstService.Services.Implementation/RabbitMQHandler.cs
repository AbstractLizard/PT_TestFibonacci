namespace FirstService.Services.Implementation
{
    using System.Threading;
    using Common.DTO;
    using Common.Interface;
    using EasyNetQ;

    /// <summary>
    /// Реализация <inheritdoc cref="IMQHandler"/> для RabbitMq
    /// </summary>
    public class RabbitMQHandler : IMQHandler
    {
        private readonly IBus _bus;

        private readonly IConfigProvider _configProvider;

        private readonly IProcessCalculateHandler _processCalculateHandler;

        private CancellationTokenSource _src;
        private CancellationToken _ct;
        
        public RabbitMQHandler(IBus bus, IConfigProvider configProvider, IProcessCalculateHandler processCalculateHandler)
        {
            _bus = bus;
            _configProvider = configProvider;
            _processCalculateHandler = processCalculateHandler;
        }

        public void Start()
        {
            _src = new CancellationTokenSource();
            _ct = _src.Token;

            _bus.PubSub
                .SubscribeAsync<MessageDto>(_configProvider.RabbitMQSubscriptionId, _processCalculateHandler.HandleMsg, _ct)
                .ConfigureAwait(false);
        }
        public void Stop()
        {
            _ct.ThrowIfCancellationRequested();
        }
    }
}