using BusLay.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Modules.Common
{
    public class SessionAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var sessionClaim = context.HttpContext.User.FindFirst("sessionId");
                var accountService = context.HttpContext.RequestServices.GetRequiredService<CustomerService>();
                if (Guid.TryParse(sessionClaim?.Value, out Guid sessionId))
                {
                    accountService.GetSession(s => s.Id == sessionId);
                }
                throw new Exception();
            }
            catch (Exception)
            {
                context.HttpContext.Response.StatusCode = 401;
                var error = new { StatusCode = 401, Message = "Session was expired!" };

                context.Result = new JsonResult(error);
            }
        }
    }
}