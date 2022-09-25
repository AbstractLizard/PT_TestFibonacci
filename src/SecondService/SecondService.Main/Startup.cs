namespace SecondService.Main
{
    using Message;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using Services;
    using Services.Implementation;
    using Utils;
    using Utils.Interface;

    /// <summary> Класс инициализации сервиса </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            var configProvider = new ConfigProvider();
            services.AddSingleton<IConfigProvider>(configProvider);
            services.AddTransient<ICalculateFibService, CalculateFibService>();
            services.AddTransient<IProcessCalculateHandler, ProcessCalculateHandler>();

            services.RegisterEasyNetQ(configProvider.RabbitMQConnString);
            services.AddSingleton<IMQHandler<MessageDto>, RabbitMQHandler<MessageDto>>();
            services.AddSingleton<IRequestHandler, RequestHandler>();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "SecondService.Main", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SecondService.Main v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}