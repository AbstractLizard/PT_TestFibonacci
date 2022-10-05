namespace Common.Interface
{
    using System.Threading.Tasks;

    /// <summary>
    /// Сервис передачи сообщения
    /// </summary>
    public interface IPublishDataService
    {
        /// <summary>
        ///  Передать данные
        /// </summary>
        Task Publish<T>(T obj);
    }
}