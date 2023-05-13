using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

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
                throw new SecurityTokenValidationException("Invalid authorization token.");
            }

            var token = authHeader.Substring("Bearer ".Length);

            try
            {
                var tokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Program.configuration["JwtKey"])),
                };

                var tokenHandler = new JwtSecurityTokenHandler();
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
                        throw new SecurityTokenValidationException("User isn't authorized to access this resource.");
                    }
                }
            }
            catch (Exception)
            {
                throw new SecurityTokenValidationException("Unauthorized. Invalid token.");
            }
        }
    }
}
