namespace Smakoowa_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Register")]
        public async Task<ServiceResponse> RegisterUser([FromBody] RegisterRequest model)
        {
            return await RegisterUser(model);
        }

        [HttpPost("Login")]
        public async Task<ServiceResponse> LoginUser([FromBody] LoginRequest model)
        {
            return await _accountService.LoginUser(model);
        }
    }
}