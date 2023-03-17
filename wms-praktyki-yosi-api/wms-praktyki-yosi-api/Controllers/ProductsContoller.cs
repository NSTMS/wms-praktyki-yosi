using Microsoft.AspNetCore.Mvc;
using System.Net;
using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Models;
using wms_praktyki_yosi_api.Services;

namespace wms_praktyki_yosi_api.Controllers
{
    [Route("api/products")]
    public class ProductsController: ControllerBase
    {

        public readonly IProductService _productService;
        public ProductsController(IProductService productService) {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            var productList = _productService.GetAll();
            return Ok(productList);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get([FromRoute]int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound(1);
            }
            return Ok(product);
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody]ProductDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var IsUpdated  = _productService.UpdateProduct(id,dto);
            if (!IsUpdated) { return NotFound(1); }
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct([FromRoute]int id)
        {
            var delete = _productService.RemoveProduct(id);
            if (!delete)
            {
                return BadRequest(1);
            }
            return Ok();
        }
        [HttpPost]
        public ActionResult AddProduct([FromBody] ProductDto dto)
        {
           
            if(!ModelState.IsValid)
            {
                return BadRequest(2);
            }
            var productId = _productService.AddNewProduct(dto);
            return Created("/api/products/" + productId, null);
        }
    }

 
}