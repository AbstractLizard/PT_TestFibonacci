namespace SecondService.Services
{
    using Message;

    /// <summary> Сервис обработки запросов </summary>
    public interface IRequestHandler
    {
        /// <summary> Запустить расчет </summary>
        /// <param name="countThread">Количество паралельных расчетов</param>
        void StartCalculate(int countThread);
        
        /// <summary> Остановить расчеты </summary>
        void StopCalculate();

        /// <summary> Обработать получение сообщения </summary>
        void HandleMsg(MessageDto msg);
    }
}