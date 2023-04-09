//using static Smakoowa_Api.Middlewares.RequestCountMiddleware;

//namespace Smakoowa_Api.Controllers
//{

//    [Route("api/[controller]")]
//    [ApiController]
//    public class QueueController
//    {
//        private readonly IRequestCounterService _requestCounterService;

//        public QueueController(IRequestCounterService requestCounterService)
//        {
//            _requestCounterService = requestCounterService;
//        }

//        [HttpPost("Enqueue")]
//        public async Task<ServiceResponse> Enqueue([FromBody] RequestDto requestDto)
//        {
//            await _requestCounterService.LogRequestCount(requestDto.ControllerName, requestDto.ActionName, requestDto.Parameters);

//            return ServiceResponse.Success();
//        }

//    }
//}
