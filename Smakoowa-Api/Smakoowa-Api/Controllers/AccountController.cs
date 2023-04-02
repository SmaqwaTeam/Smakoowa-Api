namespace Smakoowa_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;

        public AccountController(IAccountService accountService, IMapper mapper, UserManager<ApiUser> userManager)
        {
            _accountService = accountService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost("Register")]
        public async Task<ServiceResponse> RegisterUser([FromBody] CreateUserDto model)
        {
            var user = _mapper.Map<ApiUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                string errorMessage = "";
                string line = "";
                foreach (var error in result.Errors)
                {
                    line = error.Description + " ";
                    errorMessage += line;
                }
                return ServiceResponse.Error(errorMessage);
            }
            return ServiceResponse.Success("Account has been created.");
        }

        [HttpPost("Login")]
        public async Task<ServiceResponse> LoginUser([FromBody] LoginUserDto model)
        {
            return await _accountService.LoginUser(model);
        }
    }
}