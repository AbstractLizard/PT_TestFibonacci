namespace FirstService.Services
{
    using System.Collections.Concurrent;
    using Message;

    /// <summary> Обработчик очереди </summary>
    public interface IMQHandler
    {
        /// <summary>
        /// Запустить чтение из очереди
        /// </summary>
        void Start();

        /// <summary>
        /// Запустить чтение из очереди
        /// </summary>
        void Stop();
        
        /// <summary> Получить коллекцию сообщений </summary>
        BlockingCollection<MessageDto> GetBlockingCollection();
    }
}