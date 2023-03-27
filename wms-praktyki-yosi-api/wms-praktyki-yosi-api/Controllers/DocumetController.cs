using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wms_praktyki_yosi_api.Models;
using wms_praktyki_yosi_api.Models.DocumentModels;
using wms_praktyki_yosi_api.Services;

namespace wms_praktyki_yosi_api.Controllers
{
    [Route("api/documents")]
    [Authorize]
    [ApiController]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public class DocumetController : ControllerBase
    {
        private readonly IDocumentService _service;

        public DocumetController(IDocumentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<DocumentDto>>> GetAllDocuments()
        {
            var documents = _service.GetAllDocuments();
            return Ok(documents);
        }

        [HttpPost]
        public async Task<ActionResult> AddDocument([FromBody] AddDocumentDto dto)
        {
            var createdId = _service.AddDocument(dto);
            return Created("api/document/" + createdId, null);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetailedDocumentDto>> GetDocumentDetails([FromRoute]string id)
        {
            var document = _service.GetDocumentDetails(id);
            return Ok(document);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDocument([FromRoute] string id)
        {
            _service.DeleteDocument(id);
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> AddItemToDocument([FromRoute] string id, [FromBody] AddDocumentItemDto item)
        {
            _service.AddItemToDocument(id, item);
            return Ok();
        }

        [HttpPost("{id}/markasfinished")]
        public async Task<ActionResult> MarkDocumentAsFinished([FromRoute] string id, [FromBody] bool finished)
        {
            _service.MarkDocumentAsFinished(id, finished);
            return Ok();
        }

        [HttpPost("{id}/visitedlocation")]
        public async Task<ActionResult> VisitLocation(
            [FromRoute] string id,
            [FromBody] DocumentVisitLocationDto location)
        {
            _service.VisitLocation(id, location);
            return Ok();
        }

        [HttpDelete("{id}/items/{productId}")]
        public async Task<ActionResult> DeleteItemFromDocument([FromRoute] string id, [FromRoute] int productId)
        {
            _service.DeleteDocumentItem(id, productId);
            return Ok();
        }

        [HttpPut("{id}/items/{productId}")]
        public async Task<ActionResult> UpdateItemInDocument(
            [FromRoute] string id,
            [FromRoute] int productId,
            [FromBody] EditDocumentItemDto item)
        {
            _service.UpdateItemInDocument(id, productId, item);
            return Ok();
        }

    }
}
