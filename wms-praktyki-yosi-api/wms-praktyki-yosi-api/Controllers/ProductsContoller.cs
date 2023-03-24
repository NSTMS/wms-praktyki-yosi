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
    [ApiController]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public class ProductsController : ControllerBase
    {

        public readonly IProductService _productService;
        private readonly IAccountService _accountService;

        public ProductsController(IProductService productService, IAccountService accountService)
        {
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
        public ActionResult<Product> Get([FromRoute] int id)
        {
            var product = _productService.GetById(id);
            return Ok(product);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Moderator")]

        public ActionResult Update([FromRoute] int id, [FromBody] ProductDto dto)
        {
            _productService.UpdateProduct(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult DeleteProduct([FromRoute] int id)
        {
            _productService.RemoveProduct(id);
            return Ok();
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult AddProduct([FromBody] ProductDto dto)
        {
            var productId = _productService.AddNewProduct(dto);
            return Created("/api/products/" + productId, null);
        }


    }
}