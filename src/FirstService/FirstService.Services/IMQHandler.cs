namespace FirstService.Services
{
    using System.Collections.Concurrent;

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
    }
}