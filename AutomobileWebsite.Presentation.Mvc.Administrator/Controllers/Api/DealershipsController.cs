using AutomobileWebsite.BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AutomobileWebsite.Presentation.Mvc.Administrator.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealershipsController : ControllerBase
    {
        private readonly IBusinessLogics _businessLogics;

        public DealershipsController(IBusinessLogics businessLogics)
        {
            _businessLogics = businessLogics;
        }

        [HttpGet("GetDealershipById")]
        public IActionResult GetDealershipById(int dealershipId)
        {
            try
            {
                var dealership = _businessLogics.DealershipBusinessLogic.GetSingle(
                d => new
                {
                    d.DealershipName,
                    d.WebsiteUrl,
                    d.IsActive
                },
                d => d.DealershipId == dealershipId);

                if (dealership == null)
                {
                    return BadRequest("Dealership does not exits");
                }

                return Ok(dealership);
            }
            catch (Exception ex)
            {
                return BadRequest("An error has occurred");
            }
        }
    }
}