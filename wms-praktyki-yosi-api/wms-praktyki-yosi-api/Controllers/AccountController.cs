using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;
using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Exceptions;
using wms_praktyki_yosi_api.Models;
using wms_praktyki_yosi_api.Models.Validators;
using wms_praktyki_yosi_api.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace wms_praktyki_yosi_api.Controllers
{
    [Route("api/account")]
    [ApiController]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public class AccountController: ControllerBase
    {
        private readonly IAccountService _service;
        private readonly IValidator<RegisterUserDto> _validator;
        private readonly ICustomAuthorizationService _authorizationService;

        public AccountController(IAccountService service, IValidator<RegisterUserDto> validator, ICustomAuthorizationService authorizationService)
        {
            _service = service;
            _validator = validator;
            _authorizationService = authorizationService;
        }

        [HttpGet("users")]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult<List<UserDto>>> GetAll([FromQuery] GetRequestQuery query)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Forbid();

            return await _service.GetAll(query);
        }

        [HttpGet("users/{id}")]
        [Authorize]
        public async Task<ActionResult<User>> Get([FromRoute] string id)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var user = await _service.Get(id);
            return Ok(user);

        }

        [HttpPost("register")]
        [AllowAnonymous]

        public async Task<ActionResult> RegisterUser([FromBody]RegisterUserDto user)
        {
            var result = await _validator.ValidateAsync(user);

            if (!result.IsValid)
            {
                var listOfErrors = new List<string>();

                foreach (var error in result.Errors)
                {
                    listOfErrors.Add(error.ErrorCode);
                }
                return BadRequest(new
                {
                    Errors = listOfErrors
                }); ;
            }

            await _service.RegisterUser(user);
            return Ok();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody]UserLoginDto dto)
        {
            var result = await _service.GetToken(dto);
            return Ok(result);
            
        }
        
        [HttpPut("users/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateUserRole([FromRoute]string id, [FromBody] string newRole)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            
                await _service.ModifyUserRole(id, newRole);
                return Ok();
        }

        [HttpDelete("users/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteUser([FromRoute]string id)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            await _service.DeleteUser(id);
            return Ok();
        }

        [HttpGet("info")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetUserInfo()
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var userEmail = User.FindFirst(ClaimTypes.Name);
            if (userEmail is null)
                return Unauthorized();
            

            var userInfo = await _service.GetUserInfo(userEmail.Value);
            return Ok(userInfo);

        }





    }
}
