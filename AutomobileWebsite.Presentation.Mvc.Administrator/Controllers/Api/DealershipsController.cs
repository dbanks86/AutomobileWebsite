using AutomobileWebsite.BusinessLogicLayer.Interfaces;
using AutomobileWebsite.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Linq.Expressions;

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

        [HttpGet("GetDealershipAddressesByDealershipId")]
        public IActionResult GetDealershipAddressesByDealershipId(int dealershipId)
        {
            try
            {
                Expression<Func<DealershipAddress, bool>> filter;

                if (dealershipId > 0)
                {
                    filter = da => da.DealershipId == dealershipId;
                }
                else
                {
                    filter = null;
                }

                var dealershipAddresses = _businessLogics.DealershipAddressBusinessLogic.Get(
                da => new
                {
                    da.DealershipAddressId,
                    da.Street,
                    da.City,
                    da.State.StateAbbreviation,
                    da.ZipCode
                },
                filter,
                da => da.OrderBy(da2 => da2.Street));

                if (dealershipAddresses == null)
                {
                    return BadRequest("Dealership addresses do not exist");
                }

                return Ok(dealershipAddresses);
            }
            catch (Exception ex)
            {
                return BadRequest("An error has occurred");
            }
        }
    }
}