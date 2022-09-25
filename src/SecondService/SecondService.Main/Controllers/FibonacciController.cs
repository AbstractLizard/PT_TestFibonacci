namespace SecondService.Main.Controllers
{
    using System.Threading.Tasks;
    using Message;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    [ApiController]
    [Route("[controller]")]
    public class FibonacciController : ControllerBase
    {
        private readonly IRequestHandler _requestHandler;
        private readonly IMQHandler<MessageDto> _mqHandler;
        
        public FibonacciController(IRequestHandler requestHandler, IMQHandler<MessageDto> mqHandler)
        {
            _requestHandler = requestHandler;
            _mqHandler = mqHandler;
        }

        [Route("Start")]
        [HttpPost]
        public async Task<ActionResult> Start([FromBody]int countThread)
        {
            _requestHandler.StartCalculate(countThread);

            return Ok();
        }
        
        [Route("Message")]
        [HttpPost]
        public async Task<ActionResult> Message(MessageDto msg)
        {
           _requestHandler.HandleMsg(msg);
           return Ok();
        }
        
        [Route("Stop")]
        [HttpPost]
        public async Task<ActionResult> Stop()
        {
            _requestHandler.StopCalculate();
            return Ok();
        }
        
        [Route("TestMessage")]
        [HttpPost]
        public async Task<ActionResult> TestMessage(MessageDto msg)
        {
            _mqHandler.Publish(msg);
            return Ok();
        }
    }
}