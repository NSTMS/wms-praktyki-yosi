using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Models;
using wms_praktyki_yosi_api.Services;

namespace wms_praktyki_yosi_api.Controllers
{
    [Route("api/products")]
    [Authorize]
    public class ProductsController: ControllerBase
    {

        public readonly IProductService _productService;
        private readonly IAccountService _accountService;

        public ProductsController(IProductService productService, IAccountService accountService) {
            _productService = productService;
            _accountService = accountService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            var productList = _productService.GetAll();
            return Ok(productList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get([FromRoute]int id)
        {
            if (!await UserIsAuthorized(User))
                return Unauthorized();

            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound(1);
            }
            return Ok(product);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody]ProductDto dto)
        {
            if (!await UserIsAuthorized(User))
                return Unauthorized();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var IsUpdated  = _productService.UpdateProduct(id,dto);
            if (!IsUpdated) { return NotFound(1); }
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult> DeleteProduct([FromRoute]int id)
        {
            if (!await UserIsAuthorized(User))
                return Unauthorized();

            var delete = _productService.RemoveProduct(id);
            if (!delete)
            {
                return BadRequest(1);
            }
            return Ok();
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult> AddProduct([FromBody] ProductDto dto)
        {
            if (!await UserIsAuthorized(User))
                return Unauthorized();

            if (!ModelState.IsValid)
            {
                return BadRequest(2);
            }
            var productId = _productService.AddNewProduct(dto);
            return Created("/api/products/" + productId, null);
        }
        private async Task<bool> UserIsAuthorized(ClaimsPrincipal user)
        {
            var userEmail = user.FindFirst(ClaimTypes.Name);
            if (userEmail is null)
                return false;

            try
            {
                var userInfo = await _accountService.GetUserInfo(userEmail.Value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

 
}