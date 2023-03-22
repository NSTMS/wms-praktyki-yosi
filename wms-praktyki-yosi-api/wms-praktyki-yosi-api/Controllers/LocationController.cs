using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Exceptions;
using wms_praktyki_yosi_api.Models;
using wms_praktyki_yosi_api.Services;
namespace wms_praktyki_yosi_api.Controllers
{
    [Route("api/locations")]
    [Authorize]
    public class LocationController : ControllerBase
    {

        private readonly ILocationService _locationService;
        private readonly ICustomAuthorizationService _authorizationService;

        public LocationController(ILocationService locationService, ICustomAuthorizationService authorizationService)
        {
            _locationService = locationService;
            _authorizationService = authorizationService;

        }

        [HttpGet] public async Task<ActionResult> GetAll() {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var result = _locationService.GetAllLocations();
            if (result == null)
            {
                return NotFound(151);//zmień na jakis inny error code
            }
            return Ok(result);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult> AddProductToLocation([FromBody] ProductLocationDto dto)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();
            try
            {
                var result = _locationService.AddProductToLocation(dto);
                return Ok(result);
            }
            catch(BadRequestException ex)
            {
                ModelState.AddModelError("Errors", ex.Message);
                return BadRequest(ModelState);
            }
          
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Moderator")] public async Task<ActionResult> GetById([FromRoute] int id)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();
            var result = _locationService.GetLocationById(id);
            return Ok(result);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult> Edit([FromRoute] int id, [FromBody] ProductLocationDto dto) {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var result = _locationService.UpdateLocation(id, dto);
            if (!result)
            {
                return NotFound(1);//zmień na jakis inny error code
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult> Delete([FromRoute] int id) {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var result = _locationService.DeleteLocation(id);
            if (!result)
            {
                return NotFound(1);//zmień na jakis inny error code
            }
            return Ok();
        }
    }
}
