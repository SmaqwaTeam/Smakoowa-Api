using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Smakoowa_Api.Services
{
    public class ApiUserService : IApiUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetCurrentUserId()
        {
            var authHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var token = authHeader.Substring("Bearer ".Length);

            var handler = new JwtSecurityTokenHandler();
            var claims = handler.ReadJwtToken(token).Claims;
            var userIdClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            return int.Parse(userIdClaim.Value);
        }
    }
}
