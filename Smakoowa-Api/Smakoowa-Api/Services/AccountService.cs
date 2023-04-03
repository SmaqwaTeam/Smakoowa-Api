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

        public AccountService(IMapper mapper, UserManager<ApiUser> userManager, SignInManager<ApiUser> signInManager,
            AuthenticationSettings authenticationSettings, IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationSettings = authenticationSettings;
            _configuration = configuration;
        }

        public async Task<ServiceResponse> LoginUser(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByNameAsync(loginRequest.UserName);
            if (user == null)
            {
                return ServiceResponse.Error("User not found.", HttpStatusCode.Unauthorized);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, false);
            if (!result.Succeeded)
            {
                return ServiceResponse.Error("Ivalid password.", HttpStatusCode.Unauthorized);
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("AspNet.Identity.SecurityStamp", user.SecurityStamp),
            };

            List<Claim> userRoles = new();
            foreach (string role in await _userManager.GetRolesAsync(user))
            {
                userRoles.Add(new Claim(ClaimTypes.Role, role));
            }
            claims.AddRange(userRoles);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);
            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer, claims, expires: expires, signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();

            var loginResponse = new LoginResponse
            {
                Token = tokenHandler.WriteToken(token),
                User = _mapper.Map<ApiUserResponseDto>(user)
            };

            return ServiceResponse<LoginResponse>.Success(loginResponse, "Login successful.");
        }

        public async Task<ServiceResponse> RegisterUser(RegisterRequest registerRequest)
        {
            var user = _mapper.Map<ApiUser>(registerRequest);
            var result = await _userManager.CreateAsync(user, registerRequest.Password);
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

            var newUser = await _userManager.FindByEmailAsync(registerRequest.Email);
            await _userManager.AddToRoleAsync(newUser, "User");

            return ServiceResponse.Success("Account has been created.");
        }
    }
}
