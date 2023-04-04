using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Smakoowa_Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JwtAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _roles;

        public JwtAuthorizeAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                throw new SecurityTokenValidationException("Unauthorized.");
            }

            var token = authHeader.Substring("Bearer ".Length);
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Program.configuration["JwtKey"])),
                };
                var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

                if (!claimsPrincipal.Identity.IsAuthenticated)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                if (_roles.Length > 0)
                {
                    var hasRole = false;
                    foreach (var role in _roles)
                    {
                        if (claimsPrincipal.IsInRole(role))
                        {
                            hasRole = true;
                            break;
                        }
                    }

                    if (!hasRole)
                    {
                        throw new SecurityTokenValidationException("Unauthorized.");
                    }

                    var handler = new JwtSecurityTokenHandler();
                    var claims = handler.ReadJwtToken(token).Claims;

                    var userIdClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                    if (userIdClaim != null)
                    {
                        Program.configuration["CurrentUserId"] = userIdClaim.Value;
                    }
                }
            }
            catch (Exception)
            {
                throw new SecurityTokenValidationException("Unauthorized.");
            }
        }
    }
}
