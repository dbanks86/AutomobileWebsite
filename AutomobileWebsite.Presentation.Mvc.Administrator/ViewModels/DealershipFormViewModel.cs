using AutomobileWebsite.Presentation.Mvc.Administrator.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace AutomobileWebsite.Presentation.Mvc.Administrator.ViewModels
{
    public class DealershipFormViewModel : FormViewModel
    {
        [Display(Name = "Dealership")]
        public int DealershipId { get; set; }

        [Required(ErrorMessage = "Dealership name required")]
        [StringLength(255, ErrorMessage = "Dealership name must be less than 255 characters")]
        [Display(Name = "Dealership Name")]
        public string DealershipName { get; set; }

        [Required(ErrorMessage = "Website URL required")]
        [StringLength(255, ErrorMessage = "Website URL must be less than 255 characters")]
        [Url(ErrorMessage = "Invalid URL Format")]
        [Display(Name = "Website URL")]
        public string WebsiteUrl { get; set; }

        public bool IsActive { get; set; }

        public IEnumerable<SelectListItem> Dealerships { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<DealershipsController, IActionResult>> update = c => c.Update(this);

                Expression<Func<DealershipsController, IActionResult>> create = c => c.Create(this);

                var action = (DealershipId <= 0) ? create : update;

                return (action.Body as MethodCallExpression).Method.Name;
            }
        }
    }
}
