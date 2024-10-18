using System;
using System.ComponentModel.DataAnnotations;

namespace EmploymentSystem.WebApi.Attributes
{
  public class FutureDateAttribute : ValidationAttribute
  {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      if (value == null || !(value is DateTime))
      {
        return new ValidationResult("Invalid date format.");
      }

      var dateTime = (DateTime)value;

      if (dateTime <= DateTime.UtcNow)
      {
        return new ValidationResult("Expiry date must be a future date.");
      }

      return ValidationResult.Success;
    }
  }
}
