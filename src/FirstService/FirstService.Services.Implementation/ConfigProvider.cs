namespace FirstService.Services.Implementation
{
    using System;
    using Microsoft.Extensions.Configuration;

    ///<inheritdoc cref="IConfigProvider"/>
    public class ConfigProvider : IConfigProvider
    {
        public string RabbitMQConnString { get; }
        public string RabbitMQSubscriptionId { get; }
        public string SecondServiceURL { get; }
        public string SecondServiceStartURL { get; }
        public string SecondService–°alculateURL { get; }
        public string SecondServiceStopURL { get; }

        public ConfigProvider()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            
            RabbitMQConnString = configuration?.GetSection("RabbitMQ")?["ConnString"];
            
            RabbitMQSubscriptionId = configuration?.GetSection("RabbitMQ")?["SubscriptionId"];
            
            SecondServiceURL = configuration?.GetSection("SecondService")?["Url"];
            
            SecondServiceStartURL = configuration?.GetSection("SecondService")?["StartURL"];
            
            SecondService–°alculateURL = configuration?.GetSection("SecondService")?["–°alculateURL"];
            
            SecondServiceStopURL = configuration?.GetSection("SecondService")?["StopURL"];
        }
    }
}