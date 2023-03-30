using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wms_praktyki_yosi_api.Models;
using wms_praktyki_yosi_api.Models.DocumentModels;
using wms_praktyki_yosi_api.Models.Validators;
using wms_praktyki_yosi_api.Services;

namespace wms_praktyki_yosi_api.Controllers
{
    [Route("api/documents")]
    [Authorize]
    [ApiController]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _service;
        private readonly ICustomAuthorizationService _authorizationService;

        public DocumentController(IDocumentService service, ICustomAuthorizationService authorizationService)
        {
            _service = service;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DocumentDto>>> GetAllDocuments([FromQuery]GetRequestQuery query)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var documents = _service.GetAllDocuments(query);
            return Ok(documents);
        }

        [HttpPost]
        public async Task<ActionResult> AddDocument([FromBody] AddDocumentDto dto)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var createdId = _service.AddDocument(dto);
            return Created("/api/documents/" + createdId, null);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetailedDocumentDto>> GetDocumentDetails([FromRoute]string id)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var document = _service.GetDocumentDetails(id);
            return Ok(document);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteDocument([FromRoute] string id)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            _service.DeleteDocument(id);
            return Ok();
        }

        [HttpPost("{id}/markasfinished")]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult> MarkDocumentAsFinished([FromRoute] string id, [FromBody] bool finished)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            _service.MarkDocumentAsFinished(id, finished);
            return Ok();
        }

        [HttpPost("{id}/visitedlocation")]
        public async Task<ActionResult> VisitLocation(
            [FromRoute] string id,
            [FromBody] DocumentVisitLocationDto location)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            _service.VisitLocation(id, location);
            return Ok();
        }

        [HttpGet("{id}/items")]
        public async Task<ActionResult> GetDocumentItems([FromRoute] string id, [FromQuery]GetRequestQuery query)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var items = _service.GetDocumentItems(id, query);
            return Ok(items);
        }

        [HttpPost("{id}/items")]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult> AddItemToDocument([FromRoute] string id, [FromBody] AddDocumentItemDto item)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            _service.AddItemToDocument(id, item);
            return Ok();
        }

        [HttpDelete("{id}/items/{itemId}")]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult> DeleteItemFromDocument([FromRoute] string id, [FromRoute] string itemId)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            _service.DeleteDocumentItem(id, itemId);
            return Ok();
        }

        [HttpPut("{id}/items/{productId}")]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult> UpdateItemInDocument(
            [FromRoute] string id,
            [FromRoute] int productId,
            [FromBody] EditDocumentItemDto item)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            _service.UpdateItemInDocument(id, productId, item);
            return Ok();
        }

    }
}
