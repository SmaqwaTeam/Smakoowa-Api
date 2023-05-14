using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Smakoowa_Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly IApiUserMapperService _apiUserMapperService;
        private readonly IApiUserValidatorService _apiUserValidatorService;
        private readonly UserManager<ApiUser> _userManager;
        private readonly SignInManager<ApiUser> _signInManager;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IConfiguration _configuration;

        public AccountService(IApiUserMapperService mapper, UserManager<ApiUser> userManager, SignInManager<ApiUser> signInManager,
            AuthenticationSettings authenticationSettings, IConfiguration configuration, IApiUserValidatorService apiUserValidatorService, 
            IApiUserMapperService apiUserMapperService)
        {
            _apiUserMapperService = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationSettings = authenticationSettings;
            _configuration = configuration;
            _apiUserValidatorService = apiUserValidatorService;
            _apiUserMapperService = apiUserMapperService;
        }

        public async Task<ServiceResponse> LoginUser(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByNameAsync(loginRequest.UserName);
            if (user == null)
            {
                return ServiceResponse.Error("User not found.", HttpStatusCode.NotFound);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, false);
            if (!result.Succeeded)
            {
                return ServiceResponse.Error("Ivalid password.", HttpStatusCode.Unauthorized);
            }

            List<Claim> claims = GetUserClaims(user);
            List<Claim> userRoles = await GetUserRoles(user);
            claims.AddRange(userRoles);

            JwtSecurityToken token = GenerateJwtSecurityToken(claims);
            var tokenHandler = new JwtSecurityTokenHandler();

            var loginResponse = _apiUserMapperService.MapUserLoginResponse(user, tokenHandler.WriteToken(token));

            loginResponse.User.UserRoles = userRoles.Select(ur => ur.Value).ToList();

            return ServiceResponse<LoginResponse>.Success(loginResponse, "Login successful.");
        }

        private JwtSecurityToken GenerateJwtSecurityToken(List<Claim> claims)
        {
            return new JwtSecurityToken(
                _authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtAudience,
                claims,
                expires: DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_configuration["JwtKey"])),
                        SecurityAlgorithms.HmacSha256
                )
            );
        }

        private async Task<List<Claim>> GetUserRoles(ApiUser? user)
        {
            List<Claim> userRoles = new();
            foreach (string role in await _userManager.GetRolesAsync(user))
            {
                userRoles.Add(new Claim(ClaimTypes.Role, role));
            }

            return userRoles;
        }

        private static List<Claim> GetUserClaims(ApiUser? user)
        {
            return new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("AspNet.Identity.SecurityStamp", user.SecurityStamp),
            };
        }

        public async Task<ServiceResponse> RegisterUser(RegisterRequest registerRequest)
        {
            var validationResult = await _apiUserValidatorService.ValidateRegisterRequest(registerRequest);
            if(!validationResult.SuccessStatus)
            {
                return validationResult;
            }

            var createUserResult = await _userManager.CreateAsync
                (_apiUserMapperService.MapUserRegisterRequest(registerRequest), registerRequest.Password);

            if (!createUserResult.Succeeded)
            {
                string errorMessage = "";
                foreach (var error in createUserResult.Errors)
                {
                    errorMessage += error.Description + " ";
                }
                return ServiceResponse.Error(errorMessage);
            }

            var newUser = await _userManager.FindByEmailAsync(registerRequest.Email);
            await _userManager.AddToRoleAsync(newUser, "User");

            return ServiceResponse.Success("Account has been created.", HttpStatusCode.Created);
        }
    }
}
