using System.ComponentModel.DataAnnotations;

namespace FilmManagement.BL.Domain.Validation;


[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class DateRangeAttribute(int minYear = 1800, bool mustBePast = true) : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is int intValue)
        {
            if (mustBePast && intValue > DateTime.Now.Year)
                return new ValidationResult(
                    "Year should be in the past!",
                    [validationContext.MemberName]);
            if (intValue < minYear)
                return new ValidationResult(
                    $"Year must be after {minYear}!",
                    [validationContext.MemberName]);
        }
        
        if (value is DateTime dateValue)
        {
            if (mustBePast && dateValue > DateTime.Now)
            {
                return new ValidationResult(
                    "Date should be in the past!",
                    [validationContext.MemberName]);
            }

            if (dateValue.Year < minYear)
            {
                return new ValidationResult(
                    $"Date must be after {minYear}!",
                    [validationContext.MemberName]);
            }
        }
        
        return ValidationResult.Success;
    }
}