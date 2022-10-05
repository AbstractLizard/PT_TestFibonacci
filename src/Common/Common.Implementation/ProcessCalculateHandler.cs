namespace Common.Implementation
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;
    using DTO;
    using Interface;

    /// <inheritdoc cref="IProcessCalculateHandler"/>
    public class ProcessCalculateHandler : IProcessCalculateHandler
    {
        private ConcurrentDictionary<Guid, MessageDto> dataCash;

        private readonly IPublishDataService _publishDataService;

        public ProcessCalculateHandler(IPublishDataService publishDataService)
        {
            _publishDataService = publishDataService;
        }
        public void InitProcess(int count)
        {
            Stop();
            dataCash = new ConcurrentDictionary<Guid, MessageDto>(count, count);
        }
        
        public void HandleMsg(MessageDto msg)
        { 
           Task.Run(() => InnerHandleMsg(msg)).ConfigureAwait(false);
        }
        
        public void Stop()
        {
            if (dataCash == null)
            {
                return;
            }
            dataCash.Clear();
            dataCash = null;
        }

        private void InnerHandleMsg(MessageDto msg)
        {
            if (dataCash == null)
            {
                throw new Exception("Service is not start");
            }
            if (dataCash.TryGetValue(msg.Id, out var dto))
            {
                lock (dto)
                {
                    Console.WriteLine($"Расчет {dto.Id} текущее значение {dto.FibValue}, полученное значение {msg.FibValue} потоком {Thread.CurrentThread.ManagedThreadId}");
                    dto.FibValue = CalcNext(dto.FibValue, msg.FibValue);
                    Console.WriteLine($"Расчет {dto.Id} новое значение {dto.FibValue}");
                    _publishDataService.Publish(dto);
                    return;
                }
            }
            if (!dataCash.TryAdd(msg.Id, msg))
            {
                return;
            }
            lock (msg)
            {
                Console.WriteLine($"Расчет {msg.Id} текущее значение {msg.FibValue} потоком {Thread.CurrentThread.Name}");
                msg.FibValue = CalcNext(msg.FibValue, msg.FibValue);
                Console.WriteLine($"Расчет {msg.Id} новое значение {msg.FibValue}");
                _publishDataService.Publish(msg);
            }
        }

        private long CalcNext(long prevValue, long curValue)
        {
            if (curValue == 0)
            {
                return 1;
            }
            var next = prevValue + curValue;
            if (next < 0)
            {
                return 0;
            }
            return next;
        }
    }
}