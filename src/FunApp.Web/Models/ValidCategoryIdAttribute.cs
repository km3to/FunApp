using System.ComponentModel.DataAnnotations;
using FunApp.Services.Contracts;

namespace FunApp.Web.Models
{
    public class ValidCategoryIdAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var service = (ICategoriesService)validationContext.GetService(typeof(ICategoriesService));

            if (service.IsCategoryValid((int)value))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid category!");
        }
    }
}