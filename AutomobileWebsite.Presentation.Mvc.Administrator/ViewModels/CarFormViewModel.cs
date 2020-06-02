using AutomobileWebsite.Presentation.Mvc.Administrator.Controllers;
using AutomobileWebsite.Presentation.Mvc.Administrator.ViewModels.CustomValidationAttributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace AutomobileWebsite.Presentation.Mvc.Administrator.ViewModels
{
    public class CarFormViewModel : FormViewModel
    {
        [Display(Name = "Dealership")]
        public int DealershipId { get; set; }

        [Required(ErrorMessage = "A dealership address must be selected")]
        [Display(Name = "Dealership Address")]
        public int DealershipAddressId { get; set; }

        [Required(ErrorMessage = "Year required")]
        public short? Year { get; set; }

        [Required(ErrorMessage = "Make is required")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Model is required")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Trim is required")]
        public string Trim { get; set; }

        [Required(ErrorMessage = "Mileage is required")]
        [RegularExpression(@"^(((\d{1,3},)+\d{3})|\d+)$", ErrorMessage = "Mileage is not valid")]
        [MaxMileageNewCar(300)]
        public int? Mileage { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [RegularExpression(@"^\$?(((\d{1,3},)+\d{3})|\d+)(\.\d{2})?$", ErrorMessage = "Price is not valid")]
        public decimal? Price { get; set; }

        [Display(Name = "New")]
        public bool IsNew { get; set; }

        public IEnumerable<DealershipViewModel> Dealerships { get; set; }
        public IEnumerable<DealershipAddressViewModel> DealershipAddresses { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<CarsController, IActionResult>> create = c => c.Create(this);

                var action = create;

                return (action.Body as MethodCallExpression).Method.Name;
            }
        }
    }
}
