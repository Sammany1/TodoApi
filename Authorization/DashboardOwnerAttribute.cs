using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using TodoApi.Interfaces;
using Microsoft.Extensions.DependencyInjection;
namespace TodoApi.Authorization
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DashboardOwnerAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var dashboardId = context.ActionArguments["id"] as int?;
            if (!dashboardId.HasValue)
            {
                context.Result = new BadRequestResult();
                return;
            }

            var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var dashboardService = context.HttpContext.RequestServices.GetService<IDashboardService>();
            if (dashboardService == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                return;
            }

            var isOwner = await dashboardService.ValidateOwnershipAsync(dashboardId.Value, int.Parse(userId));
            if (!isOwner)
            {
                context.Result = new ForbidResult();
                return;
            }

            await next();
        }
    }
}
