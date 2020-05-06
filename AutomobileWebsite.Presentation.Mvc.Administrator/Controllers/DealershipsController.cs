using AutomobileWebsite.BusinessLogicLayer.Dtos;
using AutomobileWebsite.BusinessLogicLayer.Interfaces;
using AutomobileWebsite.Presentation.Mvc.Administrator.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomobileWebsite.Presentation.Mvc.Administrator.Controllers
{
    public class DealershipsController : Controller
    {
        private readonly IBusinessLogics _businessLogics;

        public DealershipsController(IBusinessLogics businessLogics)
        {
            _businessLogics = businessLogics;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View("DealershipForm", new DealershipFormViewModel
            {
                Heading = "Add New Dealership",
                SaveButtonText = "Create",
                Dealerships = GetDealerships()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DealershipFormViewModel dealershipViewModel)
        {
            try
            {
                dealershipViewModel.SaveButtonText = "Create";

                if (ModelState.IsValid)
                {
                    var dealerships = _businessLogics.DealershipBusinessLogic.Get(
                        d => new
                        {
                            d.DealershipName,
                            d.WebsiteUrl
                        },
                        d => d.DealershipName.Equals(dealershipViewModel.DealershipName)
                        || d.WebsiteUrl.Equals(dealershipViewModel.WebsiteUrl));

                    if (dealerships.Count() > 0)
                    {
                        foreach (var dealership in dealerships)
                        {
                            if (dealership.DealershipName.Equals(dealershipViewModel.DealershipName))
                            {
                                ModelState.AddModelError(nameof(dealershipViewModel.DealershipName), "Dealership name already exists");
                            }

                            if (dealership.WebsiteUrl.Equals(dealershipViewModel.WebsiteUrl))
                            {
                                ModelState.AddModelError(nameof(dealershipViewModel.WebsiteUrl), "Website URL already exists");
                            }
                        }

                        return View("DealershipForm", dealershipViewModel);
                    }

                    var dealershipDto = new DealershipDto
                    {
                        DealershipName = dealershipViewModel.DealershipName,
                        WebsiteUrl = dealershipViewModel.WebsiteUrl
                    };

                    _businessLogics.DealershipBusinessLogic.Add(dealershipDto);

                    _businessLogics.Save();

                    dealershipViewModel.SuccessMessage = "Dealership successfully added";
                    dealershipViewModel.DealershipName = "";
                    dealershipViewModel.WebsiteUrl = "";

                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                dealershipViewModel.ErrorMessage = "An error has occurred";
            }

            return View("DealershipForm", dealershipViewModel);
        }

        public IActionResult Edit(int dealershipId)
        {
            var dealership = _businessLogics.DealershipBusinessLogic.GetSingle(
                d => new
                {
                    d.DealershipId,
                    d.DealershipName,
                    d.WebsiteUrl,
                    d.IsActive
                },
                d => d.DealershipId == dealershipId);

            if (dealership == null)
            {
                return BadRequest("Dealership does not exist");
            }

            return View("DealershipForm", new DealershipFormViewModel
            {
                DealershipId = dealership.DealershipId,
                DealershipName = dealership.DealershipName,
                WebsiteUrl = dealership.WebsiteUrl,
                IsActive = dealership.IsActive.Value,
                Dealerships = GetDealerships(),
                Heading = "Edit Dealership",
                SaveButtonText = "Update"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(DealershipFormViewModel dealershipViewModel)
        {
            try
            {

                dealershipViewModel.Heading = "Edit Dealership";
                dealershipViewModel.SaveButtonText = "Update";

                if (!ModelState.IsValid)
                {
                    dealershipViewModel.Dealerships = GetDealerships();
                    return View("DealershipForm", dealershipViewModel);
                }

                var dealerships = _businessLogics.DealershipBusinessLogic.Get(
                        d => new
                        {
                            d.DealershipName,
                            d.WebsiteUrl
                        },
                        d => (d.DealershipName.Equals(dealershipViewModel.DealershipName) || d.WebsiteUrl.Equals(dealershipViewModel.WebsiteUrl))
                        && d.DealershipId != dealershipViewModel.DealershipId);

                if (dealerships.Count() > 0)
                {
                    foreach (var currentDealership in dealerships)
                    {
                        if (currentDealership.DealershipName.Equals(dealershipViewModel.DealershipName))
                        {
                            ModelState.AddModelError(nameof(dealershipViewModel.DealershipName), "Dealership name already exists");
                        }

                        if (currentDealership.WebsiteUrl.Equals(dealershipViewModel.WebsiteUrl))
                        {
                            ModelState.AddModelError(nameof(dealershipViewModel.WebsiteUrl), "Website URL already exists");
                        }
                    }

                    dealershipViewModel.Dealerships = GetDealerships();

                    return View("DealershipForm", dealershipViewModel);
                }

                var dealership = _businessLogics.DealershipBusinessLogic.GetSingle(
                    d => d,
                    d => d.DealershipId == dealershipViewModel.DealershipId);

                if (dealership == null)
                {
                    return NotFound("Dealership does not exists");
                }

                _businessLogics.DealershipBusinessLogic.Update(dealership, new DealershipDto
                {
                    DealershipName = dealershipViewModel.DealershipName,
                    WebsiteUrl = dealershipViewModel.WebsiteUrl,
                    IsActive = dealershipViewModel.IsActive
                });

                _businessLogics.Save();

                dealershipViewModel.SuccessMessage = "Dealership successfully updated";
                dealershipViewModel.Dealerships = GetDealerships();
            }
            catch (Exception ex)
            {
                dealershipViewModel.ErrorMessage = "An error has occurred";
            }

            return View("DealershipForm", dealershipViewModel);
        }

        private IEnumerable<SelectListItem> GetDealerships()
        {
            return _businessLogics.DealershipBusinessLogic.Get(
                d => new SelectListItem
                {
                    Value = d.DealershipId.ToString(),
                    Text = d.DealershipName
                },
                null,
                d => d.OrderBy(d2 => d2.DealershipName));
        }
    }
}