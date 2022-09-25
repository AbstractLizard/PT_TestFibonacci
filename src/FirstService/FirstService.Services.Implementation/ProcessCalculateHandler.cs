namespace FirstService.Services.Implementation
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using Message;
    using Utils.Interface;

    ///<inheritdoc cref="IProcessCalculateHandler"/>
    public class ProcessCalculateHandler : IProcessCalculateHandler
    {
        private readonly ISenderService _senderService;
        private readonly ICalculateFibService _calculateFibService;
        private readonly IConfigProvider _configProvider;

        private Thread thread = null;
        
        public ProcessCalculateHandler(
            ISenderService senderService,
            ICalculateFibService calculateFibService,
            IConfigProvider configProvider)
        {
            _senderService = senderService;
            _calculateFibService = calculateFibService;
            _configProvider = configProvider;
        }

        public void StartProcess(BlockingCollection<MessageDto> collection)
        {
            thread = new Thread(() => RunLoop(collection));
            thread.Start();
            var startDto = new MessageDto{Id = Guid.NewGuid(), FibValue = 0};
            _senderService.Send<MessageDto>(_configProvider.SecondServiceСalculateURL, startDto);
        }

        private void RunLoop(BlockingCollection<MessageDto> collection)
        {
            foreach (var item in collection.GetConsumingEnumerable())
            {
                Console.WriteLine($"Расчет {item.Id} текущее значение {item.FibValue}");
                var newDto = new MessageDto{Id = item.Id, FibValue = _calculateFibService.CalcNext(item.FibValue)};
                Console.WriteLine($"Расчет {newDto.Id} новое значение {newDto.FibValue}");
                _senderService.Send<MessageDto>(_configProvider.SecondServiceСalculateURL, newDto);
            }
        }

        public void Stop()
        {
            if (thread.IsAlive)
            {
                thread.Join();
            }
            thread = null;
        }
    }
}