namespace Smakoowa_Api.Controllers
{
    [JwtAuthorize("Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IControllerStatisticsService _controllerStatisticsService;

        public StatisticsController(IControllerStatisticsService controllerStatisticsService)
        {
            _controllerStatisticsService = controllerStatisticsService;
        }

        [HttpGet("GetAll")]
        public async Task<ServiceResponse> GetAll()
        {
            return await _controllerStatisticsService.GetAll();
        }
    }
}
