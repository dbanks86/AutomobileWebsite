using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutomobileWebsite.Presentation.Mvc.Administrator.ViewModels
{
    public class DealershipViewModel
    {
        [Required(ErrorMessage = "Dealership name required")]
        [StringLength(255, ErrorMessage = "Dealership name must be less than 255 characters")]
        [Display(Name = "Dealership Name")]
        public string DealershipName { get; set; }

        [Required(ErrorMessage = "Street required")]
        [StringLength(255, ErrorMessage = "Street must be less than 255 characters")]
        public string Street { get; set; }

        [Required(ErrorMessage = "City required")]
        [StringLength(255, ErrorMessage = "City must be less than 255 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "A state must be selected")]
        [Display(Name = "State")]
        public int StateID { get; set; }

        [Required(ErrorMessage = "Zip code required")]
        [RegularExpression(@"(^\d{5}$)|(^\d{5}-\d{4}$)", ErrorMessage = "Invalid format ('#####' or '#####-####')")]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        public List<SelectListItem> States { get; set; }

        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
    }
}
