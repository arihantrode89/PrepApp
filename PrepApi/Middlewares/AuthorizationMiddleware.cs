
using Microsoft.AspNetCore.Http.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PrepApi.Middlewares
{
    public class AuthorizationMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.GetDisplayUrl().Contains("api/token") || context.Request.GetDisplayUrl().Contains("api/register") || context.Request.GetDisplayUrl().Contains("api/pallindrome"))
            {
                await next(context);
                return;
            }
            var token = context.Request.Headers.Authorization.ToString().Split(" ");
            if(token.ToList().Count != 2 )
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token is Invalid");
                return;
            }
            var jwt = token[1];
            var tokenHandler = new JwtSecurityTokenHandler().ReadJwtToken(jwt);
            if (tokenHandler != null)
            {

                var Role = tokenHandler.Claims.Where(x => x.Type == ClaimTypes.Role).SingleOrDefault()?.Value;
                //context.User.AddIdentity(new System.Security.Claims.ClaimsIdentity(Role));
                context.User = new ClaimsPrincipal(new ClaimsIdentity(tokenHandler.Claims,"Bearer","sub",ClaimTypes.Role));
            }
            await next(context);
        }
    }

    public static class MiddlewareExtensionClass
    {
        public static void UseAuthMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<AuthorizationMiddleware>();
        }
    }
}
