using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;
using wms_praktyki_yosi_api.Exceptions;
using wms_praktyki_yosi_api.Models;
using wms_praktyki_yosi_api.Services;

namespace wms_praktyki_yosi_api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class AccountControler: ControllerBase
    {
        private readonly IAccountService _service;
        private readonly IValidator<RegisterUserDto> _validator;

        public AccountControler(IAccountService service, IValidator<RegisterUserDto> validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpGet]
        public ActionResult<List<UserDto>> GetAll()
        {
            return _service.GetAll();   
        }

        [HttpPost]
        public async Task<ActionResult> RegisterUser([FromBody]RegisterUserDto user)
        {
            var result = await _validator.ValidateAsync(user);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            try
            {
                await _service.RegisterUser(user);

            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserLoginDto dto)
        {
            try
            {
                string token = await _service.GetToken(dto);
                return Ok(new
                {
                    token = token,
                    role = "user"
                });

            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /*[HttpPut("{id}")]
        public ActionResult UpdateUserPermissions([FromRoute]int id, [FromBody]int newPermissionLevel)
        {

            var IsUpdated = _service.UpdateUser()

        }*/
    }
}
