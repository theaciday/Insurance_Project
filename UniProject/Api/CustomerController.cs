using BusLay.Extentions;
using BusLay.Forms;
using BusLay.Services;
using BusLay.View;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication2.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService customerService;
        private readonly IHttpContextAccessor accessor;
        private readonly AuthService authService;
        public CustomerController(CustomerService customerService, IHttpContextAccessor accessor)
        {
            this.customerService = customerService;
            this.accessor = accessor;
        }


        [Route("current")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<CustomerView>> CurrentUserAsync()
        {
            var accId = int.Parse(accessor.HttpContext.User.FindFirst("Id").Value);
            var customer = await customerService.FindOneAsync(ac => ac.Id == accId);

            return customer.ToView();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Registration([FromBody] RegisterForm form)
        {
            await customerService.RegisterAsync(form);
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route("delete")]
        public IActionResult DeleteUser()
        {
            var customerId = int.Parse(accessor.HttpContext.User.FindFirst("Id").Value);
            authService.Logout(customerId);
            customerService.Remove(customerId);
            return Ok();
        }

        [HttpGet]
        [Route("get-all")]
        [Authorize]
        public ActionResult<List<Customer>> GetAll()
        {
            return customerService.GetAll();
        }

        [HttpPut]
        [Authorize]
        [Route("change")]
        public ActionResult<CustomerView> ChangeCustomer([FromBody] ChangeCustomerForm form)
        {
            return customerService.Change(form).ToView();
        }

        [HttpGet]
        [Authorize]
        [Route("get-by-rating")]
        public ActionResult<List<CustomerView>> GetByRating([FromBody] CustomerFindForm findForm)
        {
            return customerService.GetByRating(findForm);
        }

        [HttpGet]
        [Authorize]
        [Route("get-by-name")]
        public ActionResult<List<CustomerView>> GetByName([FromBody] CustomerFindForm findForm)
        {
            return customerService.GetByName(findForm);
        }

        [HttpGet]
        [Authorize]
        [Route("get-by-dob")]
        public ActionResult<List<CustomerView>> GetByDOB([FromBody] CustomerFindForm findForm)
        {
            return customerService.GetByDOB(findForm);
        }


    }
}
