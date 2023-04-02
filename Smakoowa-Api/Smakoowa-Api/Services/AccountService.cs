using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Smakoowa_Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        private readonly SignInManager<ApiUser> _signInManager;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IConfiguration _configuration;

        public AccountService(IMapper mapper, UserManager<ApiUser> userManager, SignInManager<ApiUser> signInManager, AuthenticationSettings authenticationSettings, IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationSettings = authenticationSettings;
            _configuration = configuration;
        }

        public async Task<ServiceResponse> LoginUser([FromBody] LoginUserDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return ServiceResponse.Error("User not found.", HttpStatusCode.Unauthorized);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
            {
                return ServiceResponse.Error("Ivalid password or unverified e-mail.", HttpStatusCode.Unauthorized);
            }

            var config = _configuration;
            var securityStamp = _userManager.GetSecurityStampAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("AspNet.Identity.SecurityStamp", user.SecurityStamp),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtKey"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);
            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer, claims, expires: expires, signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();

            var loginUserResponse = new LoginUserResponse
            {
                Token = tokenHandler.WriteToken(token),
                User = _mapper.Map<GetUserDTO>(user)
            };

            return ServiceResponse<LoginUserResponse>.Success(loginUserResponse, "Login successful.");
        }
    }

    public class AuthenticationSettings
    {
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
        public int JwtExpireDays { get; set; }
    }

    public class LoginUserResponse
    {
        public string Token { get; set; }
        public GetUserDTO User { get; set; }
    }

    public class LoginUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class GetUserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }

    public class CreateUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<ApiUser, GetUserDTO>();
            CreateMap<CreateUserDto, ApiUser>();
        }
    }
}
