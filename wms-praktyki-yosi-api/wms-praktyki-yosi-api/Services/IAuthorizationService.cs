using System.Security.Claims;

namespace wms_praktyki_yosi_api.Services
{
    public interface ICustomAuthorizationService
    {
        Task<bool> UserIsAuthorized(ClaimsPrincipal user);
    }
}