using AutomobileWebsite.BusinessLogicLayer.Dtos;
using AutomobileWebsite.BusinessLogicLayer.Interfaces;
using AutomobileWebsite.Models.Models;
using AutomobileWebsite.Presentation.Mvc.Administrator.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AutomobileWebsite.Presentation.Mvc.Administrator.Controllers
{
    public class CarsController : Controller
    {
        private readonly IBusinessLogics _businessLogics;

        public CarsController(IBusinessLogics businessLogics)
        {
            _businessLogics = businessLogics;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View("CarForm", new CarFormViewModel
            {
                Heading = "Add Car",
                SaveButtonText = "Create",
                Dealerships = GetDealerships(),
                DealershipAddresses = GetDealershipAddresses()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CarFormViewModel carFormViewModel)
        {
            carFormViewModel.Heading = "Add Car";
            carFormViewModel.SaveButtonText = "Create";

            try
            {
                carFormViewModel.Dealerships = GetDealerships();
                
                if (!ModelState.IsValid)
                {
                    carFormViewModel.DealershipAddresses = GetDealershipAddresses(carFormViewModel.DealershipId);
                    return View("CarForm", carFormViewModel);
                }

                _businessLogics.CarBusinessLogic.Add(new CarDto
                {
                    Year = carFormViewModel.Year.Value,
                    Make = carFormViewModel.Make,
                    Model = carFormViewModel.Model,
                    Trim = carFormViewModel.Trim,
                    Mileage = carFormViewModel.Mileage.Value,
                    Price = carFormViewModel.Price.Value,
                    IsNew = carFormViewModel.IsNew,
                    DealershipAddressId = carFormViewModel.DealershipAddressId
                });

                _businessLogics.Save();

                carFormViewModel.SuccessMessage = "Car successfully added";

                ModelState.Clear();

                carFormViewModel.DealershipId = 0;
                carFormViewModel.DealershipAddressId = 0;
                carFormViewModel.DealershipAddresses = GetDealershipAddresses();
                carFormViewModel.Year = null;
                carFormViewModel.Make = string.Empty;
                carFormViewModel.Model = string.Empty;
                carFormViewModel.Trim = string.Empty;
                carFormViewModel.Mileage = null;
                carFormViewModel.Price = null;
                carFormViewModel.IsNew = false;

                return View("CarForm", carFormViewModel);
            }
            catch (Exception ex)
            {
                carFormViewModel.DealershipAddresses = GetDealershipAddresses(carFormViewModel.DealershipId);
                carFormViewModel.ErrorMessage = "An error has occurred";
            }

            return View("CarForm", carFormViewModel);
        }

        #region Common Methods
        private IEnumerable<DealershipViewModel> GetDealerships()
        {
            return _businessLogics.DealershipBusinessLogic.Get(
                d => new DealershipViewModel
                {
                    DealershipId = d.DealershipId,
                    DealershipName = d.DealershipName
                },
                d => d.IsActive.Value && d.DealershipAddresses.Count > 0,
                d => d.OrderBy(d2 => d2.DealershipName));
        }

        private IEnumerable<DealershipAddressViewModel> GetDealershipAddresses(int dealershipId = 0)
        {
            Expression<Func<DealershipAddress, bool>> filter;

            if (dealershipId == 0)
            {
                filter = da => da.IsActive.Value;
            }
            else
            {
                filter = da => da.IsActive.Value && da.DealershipId == dealershipId;
            }

            return _businessLogics.DealershipAddressBusinessLogic.Get(
                da => new DealershipAddressViewModel
                {
                    DealershipAddressId = da.DealershipAddressId,
                    Address = $"{da.Street}, {da.City}, {da.State.StateAbbreviation} {da.ZipCode}"
                },
                filter,
                d => d.OrderBy(d2 => d2.Street));
        }
        #endregion
    }
}