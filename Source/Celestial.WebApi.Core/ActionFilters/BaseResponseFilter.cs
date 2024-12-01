using Celestial.WebApi.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Celestial.WebApi.Core.ActionFilters {
    public class BaseResponseFilter : IActionFilter {
        private DateTime startTime;

        public void OnActionExecuted(ActionExecutedContext context) {
            this.startTime = DateTime.UtcNow;
        }

        public void OnActionExecuting(ActionExecutingContext context) {
            var executionTime = DateTime.UtcNow - startTime;

            if (context.Result is ObjectResult objectResult && objectResult.Value is BaseResponse baseResponse) {
                baseResponse.Execution = executionTime;
                baseResponse.Timestamp = startTime;
            }
        }
    }
}
