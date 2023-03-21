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
using wms_praktyki_yosi_api.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace wms_praktyki_yosi_api.Controllers
{
    [Route("api/account")]
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

        [HttpGet("users")]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            if (!await UserIsAuthorized(User))
                return Forbid();

            return await _service.GetAll();
        }

        [HttpGet("users/{id}")]
        [Authorize]
        public async Task<ActionResult<User>> Get([FromRoute] string id)
        {
            if (!await UserIsAuthorized(User))
                return Unauthorized();

            try
            {
                var user = await _service.Get(id);
                return Ok(user);
            }
            catch (BadRequestException ex)
            {
                ModelState.AddModelError("Errors", ex.Message);
                return BadRequest(ModelState);
            }
            catch {
                ModelState.AddModelError("Errors", "Internal Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]

        public async Task<ActionResult> RegisterUser([FromBody]RegisterUserDto user)
        {
            var result = await _validator.ValidateAsync(user);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Errors", error.ErrorCode);
                }
                return BadRequest(ModelState);
            }

            try
            {
                await _service.RegisterUser(user);
            }
            catch (BadRequestException ex)
            {
                ModelState.AddModelError("Errors", ex.Message);
                return BadRequest(ModelState);
            }
            
            return Ok();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]UserLoginDto dto)
        {
            try
            {
                var result = await _service.GetToken(dto);
                return Ok(result);

            }
            catch (BadRequestException ex)
            {
                ModelState.AddModelError("Errors", ex.Message);
                return BadRequest(ModelState);
            }
            catch
            {
                ModelState.AddModelError("Errors", "Internal Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
        }
        
        [HttpPut("users/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateUserRole([FromRoute]string id, [FromBody] string newRole)
        {
            if (!await UserIsAuthorized(User))
                return Unauthorized();

            try
            {
                await _service.ModifyUserRole(id, newRole);
                return Ok();
            }
            catch (BadRequestException ex)
            {
                ModelState.AddModelError("Errors", ex.Message);
                return BadRequest(ModelState);
            }
            catch (Exception)
            {
                ModelState.AddModelError("Errors", "Internal Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
        }

        [HttpDelete("users/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteUser([FromRoute]string id)
        {
            if (!await UserIsAuthorized(User))
                return Unauthorized();

            try
            {
                await _service.DeleteUser(id);
                return Ok();
            }
            catch (BadRequestException ex)
            {
                ModelState.AddModelError("Errors", ex.Message);
                return BadRequest(ModelState);
            }
            catch (Exception)
            {
                ModelState.AddModelError("Errors", "Internal Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
        }

        [HttpGet("info")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetUserInfo()
        {
            if (!await UserIsAuthorized(User))
                return Unauthorized();

            var userEmail = User.FindFirst(ClaimTypes.Name);
            if (userEmail is null)
                return Unauthorized();
            
            try
            {
                var userInfo = await _service.GetUserInfo(userEmail.Value);
                return Ok(userInfo);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Smt went wrong");
            }
        }

        private async Task<bool> UserIsAuthorized(ClaimsPrincipal user)
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
