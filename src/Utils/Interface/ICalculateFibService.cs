namespace Utils.Interface
{
    /// <summary>
    /// Сервис расчета числа Фиббоначи
    /// </summary>
    public interface ICalculateFibService
    {
        /// <summary>
        /// Расчитать следующее число Фиббоначи
        /// </summary>
        /// <param name="currentValue"> текущее число Фиббоначи</param>
        /// <returns>следующее от текущего число Фиббоначи</returns>
        long CalcNext(long currentValue);
    }
}