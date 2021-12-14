using BusLay.Extentions;
using BusLay.Forms;
using BusLay.Services;
using BusLay.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace UniProject.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InsuranceController : ControllerBase
    {
        private readonly InsuranceService insuranceService;
        private readonly IHttpContextAccessor accessor;
        public InsuranceController(InsuranceService insuranceService, IHttpContextAccessor accessor)
        {
            this.insuranceService = insuranceService;
            this.accessor = accessor;
        }

        [HttpGet]
        [Authorize]
        [Route("get-all")]
        public ActionResult<List<InsuranceView>> GetAll()
        {
            return insuranceService.GetAll().Select(a => a.ToView()).ToList();
        }

        [HttpPost]
        [Authorize]
        [Route("add")]
        public ActionResult<InsuranceView> AddInsurance([FromBody] InsuranceForm form)
        {
            return insuranceService.AddInsurance(form);
        }

        [HttpDelete]
        [Authorize]
        [Route("remove")]
        public IActionResult RemoveInsurance([FromBody] InsuranceForm form)
        {
            insuranceService.Remove(form);
            return Ok();
        }

        [HttpPut]
        [Authorize]
        [Route("change")]
        public ActionResult<InsuranceView> Change([FromBody] InsuranceForm form)
        {
            return insuranceService.ChangeInsuranse(form);
        }

        [HttpGet]
        [Authorize]
        [Route("get-by-max-price")]
        public ActionResult<List<InsuranceView>> GetByMaxPrice([FromBody] InsuranceFindForm findForm)
        {
            return insuranceService.GetByMaxPrice(findForm);
        }

        [HttpGet]
        [Authorize]
        [Route("get-by-min-price")]
        public ActionResult<List<InsuranceView>> GetByMinPrice([FromBody] InsuranceFindForm findForm)
        {
            return insuranceService.GetByMinPrice(findForm);
        }

        [HttpGet]
        [Authorize]
        [Route("get-by-name")]
        public ActionResult<List<InsuranceView>> GetByInsurName([FromBody] InsuranceFindForm findForm)
        {
            return insuranceService.GetByInsurName(findForm);
        }

        [HttpGet]
        [Authorize]
        [Route("get-by-type")]
        public ActionResult<List<InsuranceView>> GetByInsurType([FromBody] InsuranceFindForm findForm)
        {
            return insuranceService.GetByInsurType(findForm);
        }

        [HttpPost]
        [Authorize]
        [Route("rating-check")]
        public ActionResult<InsuranceView> CheckInsurPrice([FromBody] InsuranceForm insuranceForm)
        {
            var customerId = int.Parse(accessor.HttpContext.User.FindFirst("Id").Value);
            return insuranceService.CheckRating(insuranceForm, customerId);
        }

    }
}
