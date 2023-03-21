using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace wms_praktyki_yosi_api.Controllers
{
    [Route("api/magazine")]
    [Authorize]
    public class MagazineController : ControllerBase
    {

    }
}
