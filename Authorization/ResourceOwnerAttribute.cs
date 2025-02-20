// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Filters;
// using System.Security.Claims;
// using System.Threading.Tasks;
// using TodoApi.Interfaces;
// using TodoApi.Services;

// namespace TodoApi.Authorization
// {
//     public class ResourceOwnerAttribute : ActionFilterAttribute
//     {
//         private readonly string _resourceType;

//         public ResourceOwnerAttribute(string resourceType)
//         {
//             _resourceType = resourceType;
//         }

//         public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
//         {
//             if (!context.ActionArguments.TryGetValue("id", out var idObj))
//             {
//                 context.Result = new BadRequestResult();
//                 return;
//             }

//             var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
//             if (string.IsNullOrEmpty(userId))
//             {
//                 context.Result = new UnauthorizedResult();
//                 return;
//             }

//             var service = _resourceType switch
//             {
//                 "Dashboard" => (IDashboardService)context.HttpContext.RequestServices.GetService(typeof(IDashboardService)),
//                 "Todo" => (ITodoService)context.HttpContext.RequestServices.GetService(typeof(ITodoService)),
//                 "Subtask" => (ISubtaskService)context.HttpContext.RequestServices.GetService(typeof(ISubtaskService)),
//                 _ => null
//             };

//             if (service == null)
//             {
//                 context.Result = new StatusCodeResult(500);
//                 return;
//             }

//             var isOwner = await (service switch
//             {
//                 IDashboardService dashboard => dashboard.ValidateOwnershipAsync((int)idObj, int.Parse(userId)),
//                 ITodoService todo => todo.ValidateOwnershipAsync((int)idObj, int.Parse(userId)),
//                 ISubtaskService subtask => subtask.ValidateOwnershipAsync((int)idObj, int.Parse(userId)),
//                 _ => Task.FromResult(false)
//             });

//             if (!isOwner)
//             {
//                 context.Result = new ForbidResult();
//                 return;
//             }

//             await next();
//         }
//     }
// }
