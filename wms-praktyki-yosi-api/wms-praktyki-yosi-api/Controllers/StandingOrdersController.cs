using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Models;
using wms_praktyki_yosi_api.Models.DocumentModels;
using wms_praktyki_yosi_api.Models.StandingOrderModels;
using wms_praktyki_yosi_api.Models.Validators;
using wms_praktyki_yosi_api.Services;

namespace wms_praktyki_yosi_api.Controllers
{
    [Route("api/orders")]
    [Authorize]
    [ApiController]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public class StandingOrdersController: ControllerBase
    {
        private readonly IStandingOrdersService _service;
        private readonly ICustomAuthorizationService _authorizationService;

        public StandingOrdersController(IStandingOrdersService service, ICustomAuthorizationService authorizationService)
        {
            _service = service;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StandingOrderDto>>> GetAllOrders([FromQuery] GetRequestQuery query)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var order = _service.GetAllOrders(query);
            return Ok(order);
        }

        [HttpPost]
        [Authorize(Roles = "Moderator,Admin")]
        public async Task<ActionResult> AddStandingOrder([FromBody] AddStandingOrderDto newOrder)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var id = _service.AddStandingOrder(newOrder);
            return Created("/api/orders/" + id, null);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetailedStandingOrderDto>> GetById([FromRoute] string id)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var order = _service.GetById(id);
            return Ok(order);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Moderator,Admin")]
        public async Task<ActionResult> EditOrder([FromRoute] string id, [FromBody] EditStandingOrderDto newOrder)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            _service.EditStandingOrder(id, newOrder);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteOrder([FromRoute] string id)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            _service.DeleteStandingOrder(id);
            return Ok();
        }

        [HttpGet("{id}/items")]
        public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetItemsInOrder([FromRoute] string id, [FromQuery] GetRequestQuery query)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            var items = _service.GetItemsInOrder(id, query);
            return Ok(items);
        }


        [HttpPost("{id}/items")]
        [Authorize(Roles = "Moderator,Admin")]
        public async Task<ActionResult> AddOrderItem([FromRoute] string id, [FromBody] AddOrderItemDto newItem)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            _service.AddItemToOrder(id, newItem);
            return Ok();
        }

        [HttpPut("{id}/items/{itemId}")]
        [Authorize(Roles = "Moderator,Admin")]
        public async Task<ActionResult<IEnumerable<OrderItemDto>>> EditOrderItem([FromRoute] string id, [FromRoute] string itemId, [FromBody] EditOrderItemDto newItem)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            _service.EditOrderItem(id, itemId, newItem);
            return Ok();
        }

        [HttpDelete("{id}/items/{itemId}")]
        [Authorize(Roles = "Moderator,Admin")]
        public async Task<ActionResult> DeleteOrderItem([FromRoute] string id, [FromRoute] string itemId)
        {
            if (!await _authorizationService.UserIsAuthorized(User))
                return Unauthorized();

            _service.DeleteOrderItem(itemId);
            return Ok();
        }
    }
}
