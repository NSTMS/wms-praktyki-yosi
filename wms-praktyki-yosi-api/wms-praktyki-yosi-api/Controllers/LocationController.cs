using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Models;
using wms_praktyki_yosi_api.Services;
namespace wms_praktyki_yosi_api.Controllers
{
    [Route("api/locations")]
    [Authorize]
    public class LocationController : ControllerBase
    {

        private readonly ILocationService _locationService;

        public LocationController(LocationService locationService)
        {
            _locationService = locationService;
        }
 
        [HttpPost] public ActionResult GetAll() {
            var result = _locationService.GetAllLocations();
            if (result == null)
            {
                return NotFound(1);//zmień na jakis inny error code
            }
            return Ok(result);
        }
        [HttpGet("{id}")] public ActionResult GetById([FromRoute]int id)
        {
            var result = _locationService.GetLocationById(id);
            return Ok(result);
        }
        [HttpPut("{id}")] public ActionResult Edit([FromRoute]int id, [FromBody]ProductLocationDto dto) {
            var result = _locationService.UpdateLocation(id,dto);
            if (!result)
            {
                return NotFound(1);//zmień na jakis inny error code
            }
            return Ok("Zaktualizowano pomyślnie");  
        }
        [HttpDelete("{id}")] public ActionResult Delete([FromRoute] int id) {
            var result = _locationService.DeleteLocation(id);
            if (!result)
            {
                return NotFound(1);//zmień na jakis inny error code
            }
            return Ok("Usunięto pomyślnie");
        }
    }
}
