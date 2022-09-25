namespace SecondService.Services.Implementation
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using Message;
    using Utils.Interface;

    /// <inheritdoc cref="IProcessCalculateHandler" />
    public class ProcessCalculateHandler : IProcessCalculateHandler
    {
        private readonly IMQHandler<MessageDto> _mqHandler;
        private readonly ICalculateFibService _calculateFibService;

        public ProcessCalculateHandler(IMQHandler<MessageDto> mqHandler,
            ICalculateFibService calculateFibService)
        {
            _mqHandler = mqHandler;
            _calculateFibService = calculateFibService;
        }

        private Thread thread;
        
        public void StartProcess(BlockingCollection<MessageDto> collection)
        {
            thread = new Thread(() => RunLoop(collection));
            thread.Start();
        }

        public void Stop()
        {
            if (thread.IsAlive)
                thread.Join();
            thread = null;
        }

        private void RunLoop(BlockingCollection<MessageDto> collection)
        {
            foreach (var item in collection.GetConsumingEnumerable())
            {
                Console.WriteLine($"Расчет {item.Id} текущее значение {item.FibValue}");
                var newDto = new MessageDto {Id = item.Id, FibValue = _calculateFibService.CalcNext(item.FibValue)};
                Console.WriteLine($"Расчет {newDto.Id} новое значение {newDto.FibValue}");
                _mqHandler.Publish(newDto);
            }
        }
    }
}