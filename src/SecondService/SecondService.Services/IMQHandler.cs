namespace SecondService.Services
{
    /// <summary> Обработчик очереди </summary>
    public interface IMQHandler<T>
        where T : class
    {
        /// <summary>
        ///     Записать сообщение в MQ
        /// </summary>
        /// <param name="msg">Сообщение</param>
        void Publish(T msg);
    }
}