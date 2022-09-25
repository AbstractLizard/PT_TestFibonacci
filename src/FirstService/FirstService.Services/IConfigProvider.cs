namespace FirstService.Services
{
    /// <summary>
    /// Интерфейс конфиг провайдера
    /// </summary>
    public interface IConfigProvider
    {
        /// <summary> URL RabbitMQ </summary>
        public string RabbitMQConnString { get;  }
        
        /// <summary> Идентификтор подписки </summary>
        public string RabbitMQSubscriptionId { get;  }

        /// <summary> URL второго сервиса </summary>
        public string SecondServiceURL { get; }

        /// <summary> URL начала расчетов </summary>
        public string SecondServiceStartURL { get; }

        /// <summary> URL передачи числа </summary>
        public string SecondServiceСalculateURL { get; }
        
        /// <summary> URL остановки расчетов </summary>
        public string SecondServiceStopURL { get; }
    }
}