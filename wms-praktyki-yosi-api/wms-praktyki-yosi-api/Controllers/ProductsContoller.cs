using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<IEnumerable<Product>> Get([FromRoute]int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound("Nie znaleziono");
            }
            return Ok(product);
        }
        [HttpPut("{id}")]
        public ActionResult<IEnumerable<Product>> Update([FromRoute] int id, [FromBody]ProductDto dto)
        {
            var product = _productService.UpdateProduct(id,dto);
            if (product == null)
            {
                return NotFound("Nie znaleziono");
            }
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct([FromRoute]int id)
        {
            var delete = _productService.RemoveProduct(id);
            if (!delete)
            {
                return BadRequest("Niepoprawne id");
            }
            return Ok("pomyœlnie usuniêto");
        }
        [HttpPost]
        public ActionResult AddProduct([FromBody] ProductDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _productService.AddNewProduct(dto);
            return Ok("dodano pomyœlnie");
        }
    }

 
}