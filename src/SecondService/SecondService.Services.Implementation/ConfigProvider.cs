namespace SecondService.Services.Implementation
{
    using System;
    using Microsoft.Extensions.Configuration;

    ///<inheritdoc cref="IConfigProvider"/>
    public class ConfigProvider : IConfigProvider
    {
        public string RabbitMQConnString { get; }
        
        public ConfigProvider()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            
            RabbitMQConnString = configuration?.GetSection("RabbitMQ")?["ConnString"];
        }
    }
}