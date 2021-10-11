using OnionStructure.API.ViewModels.Response;
using OnionStructure.Service.Services.Abstract;
using OnionStructure.Contract.Utils.Abstract;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace OnionStructure.API.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, 
                                 IAccountService accountService,
                                 IRoleService roleService,
                                 IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateJwtToken(token);
            if (userId != null)
            {
                var user = await accountService.GetAccount(userId);
                var userRoles = await roleService.GetAllUserRoles(userId);

                context.Items["User"] = new AccountLoginResponseDto
                {
                    CasinoId = user.CasinoID,
                    Email = user.Email,
                    Id = user.Id,
                    Role = userRoles.Select(s=> s.RoleName).ToArray(),
                    UserName = user.UserName
                };
            }

            await _next(context);
        }
    }
}
