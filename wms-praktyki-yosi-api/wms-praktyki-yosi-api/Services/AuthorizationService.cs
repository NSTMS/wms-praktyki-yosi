using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace wms_praktyki_yosi_api.Services
{
    public class CustomAuthorizationService : ICustomAuthorizationService
    {
        private readonly IAccountService _service;

        public CustomAuthorizationService(IAccountService service)
        {
            _service = service;
        }
        public async Task<bool> UserIsAuthorized(ClaimsPrincipal user)
        {
            var userEmail = user.FindFirst(ClaimTypes.Name);
            if (userEmail is null)
                return false;
            try
            {
                var userInfo = await _service.GetUserInfo(userEmail.Value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
