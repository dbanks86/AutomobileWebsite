using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AutomobileWebsite.Presentation.Mvc.Administrator.ViewModels.CustomValidationAttributes
{
    public class MaxMileageNewCarAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly int _maxMileage;

        public MaxMileageNewCarAttribute(int maxMileage)
        {
            _maxMileage = maxMileage;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-maxmileagenewcar", $"Max mileage for new cars is { _maxMileage }");
            context.Attributes.Add("data-val-maxmileagenewcar-maxmileage", _maxMileage.ToString());
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var carFormViewModel = validationContext.ObjectInstance as CarFormViewModel;

            if (carFormViewModel.IsNew && carFormViewModel.Mileage > _maxMileage)
            {
                return new ValidationResult($"Max mileage for new cars is {_maxMileage}");
            }

            return ValidationResult.Success;
        }
    }
}
