using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Web.Http.ModelBinding;
using wms_praktyki_yosi_api.Exceptions;
using wms_praktyki_yosi_api.Services;

namespace wms_praktyki_yosi_api.Middleweare
{

    public class ErrorHandlingMiddleweare : IMiddleware
    {
        private readonly IAccountService _accountService;

        public ErrorHandlingMiddleweare(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                
                await next.Invoke(context);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new
                {
                    Errors = new List<string> { ex.Message }
                });
            }
            catch (BadRequestException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new
                {
                    Errors = new List<string> { ex.Message }
                }); 
            }

            catch (UnauthorizedUserException ex) 
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new
                {
                    Errors = new List<string> { ex.Message }
                });
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Internal server error");
            }
        }

        private async Task<bool> IsUserAuthorized(ClaimsPrincipal user)
        {
            var userEmail = user.FindFirst(ClaimTypes.Name);
            if (userEmail is null)
                return false;

            try
            {
                var userInfo = await _accountService.GetUserInfo(userEmail.Value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}
