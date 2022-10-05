namespace Common.DTO
{
    using System;

    /// <summary>
    /// DTO для передачи данных для Расчета числа Фиббоначи
    /// </summary>
    public class MessageDto
    {
        /// <summary>
        /// Идентификатор расчета
        /// </summary>
        public Guid Id { get; init; }
        
        /// <summary>
        /// Текущее значение числа Фиббоначи
        /// </summary>
        public long FibValue { get; set; }
    }
}