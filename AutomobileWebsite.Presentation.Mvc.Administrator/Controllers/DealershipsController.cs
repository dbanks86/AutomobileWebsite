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
            DealershipViewModel dealershipViewModel = new DealershipViewModel();
            dealershipViewModel.States = GetStates();

            return View(dealershipViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DealershipViewModel dealershipViewModel)
        {
            try
            {
                dealershipViewModel.States = GetStates();

                if (ModelState.IsValid)
                {
                    var dealershipName = _businessLogics.DealershipBusinessLogic.GetSingle(
                        d => new
                        {
                            d.DealershipName
                        },
                        d => d.DealershipName.Equals(dealershipViewModel.DealershipName));

                    if (dealershipName != null)
                    {
                        ModelState.AddModelError(nameof(dealershipViewModel.DealershipName), "Dealership name already exists");

                        return View(dealershipViewModel);
                    }

                    var dealershipDto = new DealershipDto
                    {
                        DealershipName = dealershipViewModel.DealershipName,
                        DealershipAddressDto = new DealershipAddressDto
                        {
                            Street = dealershipViewModel.Street,
                            City = dealershipViewModel.City,
                            StateId = dealershipViewModel.StateID,
                            ZipCode = dealershipViewModel.ZipCode
                        }
                    };

                    _businessLogics.DealershipBusinessLogic.Add(dealershipDto);

                    _businessLogics.Save();

                    dealershipViewModel.SuccessMessage = "Dealership successfully added";
                }
            }
            catch (Exception ex)
            {
                dealershipViewModel.States = GetStates();
                dealershipViewModel.ErrorMessage = "An error has occurred";
            }

            return View(dealershipViewModel);
        }

        private List<SelectListItem> GetStates()
        {
            var states = _businessLogics.StateBusinessLogic.Get(
                        s => new SelectListItem
                        {
                            Value = s.StateId.ToString(),
                            Text = s.StateName
                        })
                        .ToList();

            states.Insert(0, new SelectListItem
            {
                Value = "",
                Text = "Select a State"
            });

            return states;
        }
    }
}