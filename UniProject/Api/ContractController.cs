using BusLay.Extentions;
using BusLay.Forms;
using BusLay.Services;
using BusLay.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace UniProject.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IHttpContextAccessor accessor;
        private readonly ContractService contractService;
        public ContractController(ContractService contractService, IHttpContextAccessor accessor)
        {
            this.contractService = contractService;
            this.accessor = accessor;
        }

        [HttpPost]
        [Route("create")]
        [Authorize]
        public ActionResult<ContractView> CreateContract([FromBody] ContractForm form)
        {
            return contractService.CreateContract(form);
        }

        [HttpPut]
        [Authorize]
        [Route("change")]
        public ActionResult<ContractView> ChangeContract([FromBody] ContractForm form)
        {
            return contractService.Change(form).ToView();
        }

        [HttpDelete]
        [Authorize]
        [Route("remove")]
        public IActionResult RemoveContract([FromBody] ContractForm form)
        {
            contractService.RemoveContract(form);
            return Ok();
        }

        [HttpGet]
        [Authorize]
        [Route("get-all")]
        public ActionResult<List<ContractView>> GetAll()
        {
            var customerId = int.Parse(accessor.HttpContext.User.FindFirst("Id").Value);
            return contractService.GetAllAsync(customerId).Result;
        }

        [HttpGet]
        [Authorize("max-price")]
        public ActionResult<List<ContractView>> ByMaxPrice([FromBody] ContractFindForm form)
        {
            return contractService.GetByMaxPrice(form);
        }

        [HttpGet]
        [Authorize("mix-price")]
        public ActionResult<List<ContractView>> ByMinPrice([FromBody] ContractFindForm form)
        {
            return contractService.GetByMinPrice(form);
        }


        [HttpGet]
        [Authorize("price")]
        public ActionResult<List<ContractView>> ByPrice([FromBody] ContractFindForm form)
        {
            return contractService.GetByPrice(form);
        }


        [HttpGet]
        [Authorize("duration")]
        public ActionResult<List<ContractView>> ByDuration([FromBody] ContractFindForm form)
        {
            return contractService.GetByDuration(form);
        }







    }
}
