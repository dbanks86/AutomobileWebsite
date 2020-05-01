using AutomobileWebsite.BusinessLogicLayer.Dtos;
using AutomobileWebsite.BusinessLogicLayer.Interfaces;
using AutomobileWebsite.Presentation.Mvc.Administrator.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
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
            DealershipViewModel dealershipViewModel = new DealershipViewModel();

            return View(dealershipViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DealershipViewModel dealershipViewModel)
        {
            try
            {

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

                        return View(dealershipViewModel);
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

            return View(dealershipViewModel);
        }
    }
}