namespace Smakoowa_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        private readonly DataContext _context;
        private readonly RoleManager<ApiRole> _roleManager;

        public AccountController(IAccountService accountService, IMapper mapper, UserManager<ApiUser> userManager, DataContext context, RoleManager<ApiRole> roleManager)
        {
            _accountService = accountService;
            _mapper = mapper;
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
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

            var newUser = await _userManager.FindByEmailAsync(model.Email);
            //var role = await _roleManager.FindByNameAsync("User");

            //await _context.UserRoles.AddAsync(new IdentityUserRole<int> { RoleId = role.Id, UserId = newUser.Id });
            await _userManager.AddToRoleAsync(newUser, "User");

            return ServiceResponse.Success("Account has been created.");
        }

        [HttpPost("Login")]
        public async Task<ServiceResponse> LoginUser([FromBody] LoginUserDto model)
        {
            return await _accountService.LoginUser(model);
        }
    }
}