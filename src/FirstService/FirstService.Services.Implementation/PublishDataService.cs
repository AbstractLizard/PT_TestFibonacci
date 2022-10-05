namespace FirstService.Services.Implementation
{
    using System.Net.Http;
    using System.Net.Mime;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Common.Interface;

    ///<inheritdoc cref="IPublishDataService"/>
    public class PublishDataService : IPublishDataService
    {
        private readonly ISenderService _senderService;

        private readonly IConfigProvider _configProvider;

        public PublishDataService(ISenderService senderService, IConfigProvider configProvider)
        {
            _senderService = senderService;
            _configProvider = configProvider;
        }

        public async Task Publish<T>(T obj)
        {
            await _senderService.Send(_configProvider.SecondServiceСalculateURL, obj).ConfigureAwait(false);
        }
    }
}