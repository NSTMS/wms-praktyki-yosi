﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using wms_praktyki_yosi_api.Services;
using wms_praktyki_yosi_api.Models;
using Microsoft.EntityFrameworkCore;
using wms_praktyki_yosi_api.Enitities;
using AutoMapper.Configuration.Conventions;
using wms_praktyki_yosi_api.Models.Validators;

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
        public async Task<ActionResult> GetAllMagazines([FromQuery]GetRequestQuery query)
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

            var res =  _magazineService.GetById(id);
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
        public async Task<ActionResult> GetLocationsOfProductInMagazineGotFromId([FromRoute]int id, [FromRoute]int prodId)
        { 
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var res = _magazineService.GetLocationsOfProduct(id, prodId);
            return Ok(res);
        }
        [HttpGet("{id}/products")]
        public async Task<ActionResult> GetMagazineProductsByID([FromRoute] int id)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var res = _magazineService.GetProductsInMagazine(id);
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

    }
}
