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
                context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                return;
            }

            var token = authHeader.Substring("Bearer ".Length);
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Program.configuration["JwtKey"])),
                };
                var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

                if (!claimsPrincipal.Identity.IsAuthenticated)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                //var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                //var role = claimsPrincipal.FindFirst(ClaimTypes.Role)?.Value;

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
                        context.Result = new ForbidResult();
                        return;
                    }
                }
            }
            catch (Exception)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            //    if (userId == null || (_role != null && role != _role))
            //    {
            //        throw new SecurityTokenException("User cannot access this method.");
            //        //context.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();
            //        //return;
            //    }
            //}
            //catch
            //{
            //    throw new SecurityTokenException("Unauthorized.");
            //    //context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
            //    //return;
            //}
        }
    }
}
