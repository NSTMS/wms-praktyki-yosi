using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using wms_praktyki_yosi_api.Exceptions;

namespace wms_praktyki_yosi_api.Models
{
    public class ValidationFilterAttribute : IActionFilter
    {


        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult("2");
            }


        }
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
