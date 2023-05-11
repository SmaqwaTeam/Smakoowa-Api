namespace Smakoowa_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IApiUserGetterService _apiUserGetterService;

        public UsersController(IApiUserGetterService apiUserGetterService)
        {
            _apiUserGetterService = apiUserGetterService;
        }

        [HttpGet("GetUserById/{userId}")]
        public async Task<ServiceResponse> GetUserById(int userId)
        {
            return await _apiUserGetterService.GetUserById(userId);
        }
    }
}
