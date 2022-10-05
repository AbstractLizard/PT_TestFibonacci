namespace SecondService.Main.Controllers
{
    using System.Threading.Tasks;
    using Common.DTO;
    using Common.Interface;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Контроллер инициализации и запуска
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class FibonacciController : ControllerBase
    {
         private readonly IProcessCalculateHandler _processCalculateHandler;
         private readonly IPublishDataService _publishDataService;
        
        public FibonacciController(IProcessCalculateHandler processCalculateHandler, 
            IPublishDataService publishDataService)
        {
            _processCalculateHandler = processCalculateHandler;
            _publishDataService = publishDataService;

        }

        [Route("Start")]
        [HttpPost]
        public Task<ActionResult> Start([FromBody]int countThread)
        {
            _processCalculateHandler.InitProcess(countThread);

            return Task.FromResult<ActionResult>(Ok());
        }
        
        [Route("Message")]
        [HttpPost]
        public async Task<ActionResult> Message(MessageDto msg)
        {
            _processCalculateHandler.HandleMsg(msg);
            return Ok();
        }
        
        [Route("Stop")]
        [HttpPost]
        public async Task<ActionResult> Stop()
        {
            _processCalculateHandler.Stop();
            return Ok();
        }
        
        [Route("TestMessage")]
        [HttpPost]
        public async Task<ActionResult> TestMessage(MessageDto msg)
        {
            await _publishDataService.Publish(msg);
            return Ok();
        }
    }
}