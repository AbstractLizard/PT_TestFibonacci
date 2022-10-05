namespace Common.Utils
{
    using System;

    /// <summary>
    /// Методы расширения для удобства
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Получить сервис из контейнера
        /// </summary>
        public static T ResolveService<T>(this IServiceProvider serviceCollection)
        {
            var service = serviceCollection.GetService(typeof(T));

            if (service == null)
            {
                throw new InvalidOperationException($"Сервис {typeof(T)} не зарегистрирован");
            }
            
            return (T)service;
        }
    }
}