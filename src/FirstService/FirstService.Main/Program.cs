namespace FirstService.Main
{
    using System;
    using System.Collections.Generic;
    using Services;
    using Utils;

    class Program
    {
        static void Main(string[] args)
        {
            Startup.InitializeApplicationContext();
            
            Console.WriteLine("Good day!");
            int numThread;

            var handlers = new List<IProcessCalculateHandler>();

            var mqHandler = Startup.ServiceProvider.ResolveService<IMQHandler>();

            mqHandler.Start();
            
            var config = Startup.ServiceProvider.ResolveService<IConfigProvider>();
            
            while(true)
            {
                Console.WriteLine("Введите число потоков:");
                var tryParseNumThread = int.TryParse(Console.ReadLine(), out numThread);

                if (!tryParseNumThread || numThread <= 0)
                {
                    Console.Write("Ошибка ввода");
                }
                else
                {
                    var res = Startup.ServiceProvider.ResolveService<ISenderService>().Send(config.SecondServiceStartURL, numThread);

                    res.Wait();
                    Console.Write($"{res.Result}");
                    break;
                }
            }
            
            Console.WriteLine("Старт расчета, для осстановки нажмите ESC");

            for (int i = 0; i < numThread; i++)
            {
                var processCalculateHandler = Startup.ServiceProvider.ResolveService<IProcessCalculateHandler>(); 
                processCalculateHandler.StartProcess(mqHandler.GetBlockingCollection());
                handlers.Add(processCalculateHandler);
            }

            while (true)
            {
                var cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
            
            Console.WriteLine("Остановка расчетов");
            
            mqHandler.Stop();

            foreach (var handler in handlers)
            {
                handler.Stop();
            }
            
            handlers.Clear();
            Startup.ServiceProvider.ResolveService<ISenderService>().Send<object>(config.SecondServiceStopURL, null);
        }
    }
}