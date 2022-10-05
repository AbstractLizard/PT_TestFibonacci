namespace FirstService.Main
{
    using System;
    using Common.DTO;
    using Common.Interface;
    using Common.Utils;
    using Services;

    class Program
    {
        static void Main(string[] args)
        {
            Startup.InitializeApplicationContext();
            
            Console.WriteLine("Good day!");
            
            int countCalc;
            
            var mqHandler = Startup.ServiceProvider.ResolveService<IMQHandler>();

            mqHandler.Start();
            
            var config = Startup.ServiceProvider.ResolveService<IConfigProvider>();
            
            while(true)
            {
                Console.WriteLine("Введите число потоков:");
                var tryParseNumThread = int.TryParse(Console.ReadLine(), out countCalc);

                if (!tryParseNumThread || countCalc <= 0)
                {
                    Console.Write("Ошибка ввода");
                }
                else
                {
                    var res = Startup.ServiceProvider.ResolveService<ISenderService>().Send(config.SecondServiceStartURL, countCalc);

                    res.Wait();
                    Console.Write($"{res.Result}");
                    break;
                }
            }
            
            Console.WriteLine("Старт расчета, для осстановки нажмите ESC");

            var processCalculateHandler = Startup.ServiceProvider.ResolveService<IProcessCalculateHandler>(); 
            processCalculateHandler.InitProcess(countCalc);

            for (int i = 0; i < countCalc; i++)
            {
                var startMsg = new MessageDto {Id = Guid.NewGuid(), FibValue = 0};
                processCalculateHandler.HandleMsg(startMsg);
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
            
            var resStop = Startup.ServiceProvider.ResolveService<ISenderService>().Send<object>(config.SecondServiceStopURL, null);
            resStop.Wait();
            Console.Write($"{resStop.Result}");
            
            mqHandler.Stop();
            processCalculateHandler.Stop();
        }
    }
}