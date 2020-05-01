using System.ComponentModel.DataAnnotations;

namespace AutomobileWebsite.Presentation.Mvc.Administrator.ViewModels
{
    public class DealershipViewModel
    {
        [Required(ErrorMessage = "Dealership name required")]
        [StringLength(255, ErrorMessage = "Dealership name must be less than 255 characters")]
        [Display(Name = "Dealership Name")]
        public string DealershipName { get; set; }

        [Required(ErrorMessage = "Website URL required")]
        [StringLength(255, ErrorMessage = "Website URL must be less than 255 characters")]
        [Url(ErrorMessage = "Invalid URL Format")]
        [Display(Name = "Website URL")]
        public string WebsiteUrl { get; set; }

        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
    }
}
