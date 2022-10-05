namespace Common.Interface
{
    using DTO;

    /// <summary> Обработчик процесса расчета </summary>
    public interface IProcessCalculateHandler
    {
        /// <summary> Запустить процесс расчета </summary>
        void InitProcess(int count);
        
        /// <summary> Обработать получение сообщения </summary>
        void HandleMsg(MessageDto msg);
        
        /// <summary> Завершить процесс расчета </summary>
        void Stop();
    }
}