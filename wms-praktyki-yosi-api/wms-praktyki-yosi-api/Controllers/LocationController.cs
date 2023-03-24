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
    [ApiController]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
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
            return Ok(result);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult> AddProductToLocation([FromBody] ProductLocationDto dto)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var result = _locationService.AddProductToLocation(dto);
            return Ok(result);

          
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

            _locationService.UpdateLocation(id, dto);
            return Ok();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult> Delete([FromRoute] int id) {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            _locationService.DeleteLocation(id);
            return Ok();
        }
    }
}
