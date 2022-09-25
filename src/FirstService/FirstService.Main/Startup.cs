namespace FirstService.Main
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Services.Implementation;
    using Utils;
    using Utils.Interface;

    /// <summary> Класс инициализации сервиса </summary>
    public static class Startup
    {
        public static IServiceCollection ServiceCollection { get; private set; }
        public static IServiceProvider ServiceProvider { get; private set; }

        public static void InitializeApplicationContext()
        {
            ServiceCollection = new ServiceCollection();
            var configProvider = new ConfigProvider();
            ServiceCollection.AddSingleton<IConfigProvider>(configProvider);
            ServiceCollection.AddHttpClient<ISenderService, SenderService>(c =>
            {
                c.BaseAddress = new Uri(configProvider.SecondServiceURL);
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            
            ServiceCollection.AddTransient<ICalculateFibService, CalculateFibService>();
            ServiceCollection.AddTransient<IProcessCalculateHandler, ProcessCalculateHandler>();

            ServiceCollection.RegisterEasyNetQ(configProvider.RabbitMQConnString);
            ServiceCollection.AddSingleton<IMQHandler, RabbitMQHandler>();
            ServiceProvider = ServiceCollection.BuildServiceProvider();
        }
    }
}