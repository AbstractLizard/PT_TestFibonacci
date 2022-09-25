namespace SecondService.Services
{
    /// <summary> Интерфейс конфиг провайдера </summary>
    public interface IConfigProvider
    {
        /// <summary> URL RabbitMQ </summary>
        public string RabbitMQConnString { get;  }
    }
}