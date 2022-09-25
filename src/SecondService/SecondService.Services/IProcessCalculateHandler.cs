namespace SecondService.Services
{
    using System.Collections.Concurrent;
    using Message;

    /// <summary> Обработчик процесса расчета </summary>
    public interface IProcessCalculateHandler
    {
        /// <summary> Запустить процесс расчета </summary>
        void StartProcess(BlockingCollection<MessageDto> collection);
        
        /// <summary> Завершить процесс расчета </summary>
        void Stop();
    }
}