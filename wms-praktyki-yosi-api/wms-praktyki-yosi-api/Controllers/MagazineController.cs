using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using wms_praktyki_yosi_api.Services;
using wms_praktyki_yosi_api.Models;
using Microsoft.EntityFrameworkCore;
using wms_praktyki_yosi_api.Enitities;
using AutoMapper.Configuration.Conventions;
using wms_praktyki_yosi_api.Models.Validators;
using wms_praktyki_yosi_api.Models.MagazineModels;
using System.Web;

namespace wms_praktyki_yosi_api.Controllers
{
    [Route("api/magazines")]
    [Authorize]
    [ApiController]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public class MagazineController : ControllerBase
    {
        private readonly IMagazineService _magazineService;
        private readonly ICustomAuthorizationService _authorizationService;

        public MagazineController(IMagazineService magazineService, ICustomAuthorizationService authorizationService)
        {
            _magazineService = magazineService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllMagazines([FromQuery] GetRequestQuery query)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var res = _magazineService.GetAll(query);
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetMagazineById([FromRoute] int id)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var res = _magazineService.GetById(id);
            // name address list of locations 
            return Ok(res);
        }
        [HttpGet("{id}/locations")]
        public async Task<ActionResult> GetMagazineLocationsByID([FromRoute] int id)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var res = _magazineService.GetLocationsInMagazine(id);
            return Ok(res);
        }
        [HttpGet("{id}/locations/{prodId}")]
        public async Task<ActionResult> GetLocationsOfProductInMagazineGotFromId([FromRoute] int id, [FromRoute] int prodId)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var res = _magazineService.GetLocationsOfProduct(id, prodId);
            return Ok(res);
        }
        [HttpGet("{id}/products")]
        public async Task<ActionResult> GetMagazineProductsByID([FromRoute] int id, [FromQuery] GetRequestQuery query)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var res = _magazineService.GetProductsInMagazine(id, query);
            return Ok(res);
        }
        [HttpGet("{id}/products/{prodId}")]
        public async Task<ActionResult> GetProductFromMagazine([FromRoute] int id, [FromRoute] int prodId)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var res = _magazineService.GetProductInMagazine(id, prodId);
            return Ok(res);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult> AddNewMagazine([FromBody] MagazineDto dto)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var res = _magazineService.AddMagzine(dto);
            return Created(res.ToString(), null);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult> UpdateMagazine([FromRoute] int id, [FromBody] EditMagazineDto dto)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            _magazineService.UpdateMagazine(id, dto);
            return Ok();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult> DeleteMagazine([FromRoute] int id)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            _magazineService.DeleteMagazine(id);
            return Ok();
        }

        [HttpGet("{id}/shelves")]
        public async Task<ActionResult<IEnumerable<ShelfDto>>> GetSelvesInMagazine([FromRoute] int id, [FromQuery] GetRequestQuery query)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var shelves = _magazineService.GetShelvesInMagazine(id, query);
            return Ok(shelves);
        }

        [HttpGet("{id}/shelves/{position}")]
        public async Task<ActionResult<DetailedShelfDto>> GetSelfInMagazine([FromRoute] int id, [FromRoute] string position)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            position = HttpUtility.UrlDecode(position);

            var shelf = _magazineService.GetShelfInMagazine(id, position);
            return Ok(shelf);
        }

        [HttpPost("{id}/shelves/{position}/move")]
        public async Task<ActionResult<DetailedShelfDto>> MoveShelfTo([FromRoute] int id, [FromRoute] string position, [FromBody] string newPosition)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            position = HttpUtility.UrlDecode(position);
            position = HttpUtility.UrlDecode(position);

            _magazineService.MoveShelfTo(id, position, newPosition);
            return Ok();
        }
    }
}
