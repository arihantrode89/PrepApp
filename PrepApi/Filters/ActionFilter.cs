using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PrepApi.Filters
{
    public class ActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var queryParams = context.HttpContext.Request.Query["queryParm"];
            if (string.IsNullOrEmpty(queryParams))
            {
                context.HttpContext.Response.StatusCode = 400;
                context.HttpContext.Response.WriteAsync("Bad Request");
                context.Result = new BadRequestResult();
                
            }
        }
    }
}
