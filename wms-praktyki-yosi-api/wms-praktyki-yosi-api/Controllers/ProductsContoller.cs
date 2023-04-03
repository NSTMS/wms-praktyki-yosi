using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Models;
using wms_praktyki_yosi_api.Models.ProductModels;
using wms_praktyki_yosi_api.Models.Validators;
using wms_praktyki_yosi_api.Services;

namespace wms_praktyki_yosi_api.Controllers
{
    [Route("api/products")]
    [Authorize]
    [ApiController]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public class ProductsController : ControllerBase
    {

        public readonly IProductService _productService;
        private readonly IAccountService _accountService;
        private readonly ICustomAuthorizationService _authorizationService;

        public ProductsController(
            IProductService productService,
            IAccountService accountService,
            ICustomAuthorizationService authorizationService)
        {

            _productService = productService;
            _accountService = accountService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll([FromQuery] GetRequestQuery query)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var productList = _productService.GetAll(query);
            return Ok(productList);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult> AddProductAsync([FromBody] ProductDto dto)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var productId = _productService.AddNewProduct(dto);
            return Created("/api/products/" + productId, null);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetAsync([FromRoute] int id)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var product = _productService.GetById(id);
            return Ok(product);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Moderator")]

        public async Task<ActionResult> UpdateAsync([FromRoute] int id, [FromBody] ProductDto dto)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            _productService.UpdateProduct(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult> DeleteProductAsync([FromRoute] int id)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            _productService.RemoveProduct(id);
            return Ok();
        }

        [HttpGet("{id}/locations")]
        public async Task<ActionResult<List<ProductLocationDto>>> GetLocationsOfProduct([FromRoute] int id, [FromQuery] GetRequestQuery query)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var productLocations = _productService.GetProductLocations(id, query);
            return Ok(productLocations);
        }



    }
}