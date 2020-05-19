using AutomobileWebsite.Presentation.Mvc.Administrator.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace AutomobileWebsite.Presentation.Mvc.Administrator.ViewModels
{
    public class DealershipAddressFormViewModel : FormViewModel
    {
        [Required(ErrorMessage = "A dealership must be selected")]
        [Display(Name = "Dealership")]
        public int DealershipId { get; set; }

        [Required(ErrorMessage = "A dealership must be selected")]
        [Display(Name = "Dealership (Edit)")]
        public int DealershipIdNew { get; set; }

        [Required(ErrorMessage = "A dealership address must be selected")]
        [Display(Name = "Dealership Address")]
        public int DealershipAddressId { get; set; }

        [Required(ErrorMessage = "Street required")]
        [StringLength(255, ErrorMessage = "Street must be less than 255 characters")]
        public string Street { get; set; }

        [Required(ErrorMessage = "City required")]
        [StringLength(255, ErrorMessage = "City must be less than 255 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "A state must be selected")]
        [Display(Name = "State")]
        public int StateId { get; set; }

        [Required(ErrorMessage = "Zip code required")]
        [RegularExpression(@"(^\d{5}$)|(^\d{5}-\d{4}$)", ErrorMessage = "Invalid format ('#####' or '#####-####')")]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public bool IsShowDealershipsEdit { get; set; }

        public List<SelectListItem> Dealerships { get; set; }
        public List<SelectListItem> DealershipsEdit { get; set; }
        public List<SelectListItem> DealershipAddresses { get; set; }
        public List<SelectListItem> States { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<DealershipsController, IActionResult>> create = c => c.CreateDealershipAddress(this);

                Expression<Func<DealershipsController, IActionResult>> update = c => c.UpdateDealershipAddress(this);

                var action = (DealershipId > 0) ? update : create;

                return (action.Body as MethodCallExpression).Method.Name;
            }
        }
    }
}
