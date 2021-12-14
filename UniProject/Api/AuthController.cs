using BusLay.Extentions;
using BusLay.Forms;
using BusLay.Services;
using BusLay.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UniProject.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IHttpContextAccessor accessor;
        private readonly CustomerService customerService;
        private readonly AuthService authService;
        public AuthController(CustomerService customerService, AuthService authService, IHttpContextAccessor accessor)
        {
            this.customerService = customerService;
            this.authService = authService;
            this.accessor = accessor;
        }

        [HttpPost]
        [Route("auth")]
        public ActionResult<TokensView> Auth([FromBody] AuthForm form)
        {
            var account = customerService.FindOneAsync(acc => acc.Username == form.UserName);
            if (account == null)
                return BadRequest("wrong email");
            var result = customerService.IsPasswordMatch(form.Password, account.Result.Id);
            if (!result)
                return BadRequest("password is wrong");

            var returnedResult = authService.Login(form);

            return returnedResult;
        }

        [HttpPost]
        [Route("refresh")]
        public ActionResult<TokensView> RefreshToken([FromBody] TokenForm form)
        {
            var session = authService.GetSession(x => x.RefreshToken == form.RefreshToken);
            if (session == null)
                return Unauthorized("invalid token!");

            return authService.CreateTokens(session.Customer);
        }

        [HttpDelete]
        [Authorize]
        [Route("logout")]
        public IActionResult Logout()
        {
            var customerId = int.Parse(accessor.HttpContext.User.FindFirst("Id").Value);
            authService.Logout(customerId);
            return Ok();
        }

        [HttpGet]
        [Authorize]
        [Route("current-acc")]
        public async Task<ActionResult<CustomerView>> GetCurrentAsync()
        {
            var cusId = int.Parse(accessor.HttpContext.User.FindFirst("Id").Value);
            var customer = await customerService.FindOneAsync(ac => ac.Id == cusId);

            return customer.ToView();
        }
    }
}
