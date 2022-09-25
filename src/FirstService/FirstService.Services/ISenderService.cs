namespace FirstService.Services
{
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Сервис отправки сообщений
    /// </summary>
    public interface ISenderService
    {
        /// <summary>
        /// Отправить данные 
        /// </summary>
        Task<HttpResponseMessage> Send<T>(string url, T obj);
    }
}