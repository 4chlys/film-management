using System.ComponentModel.DataAnnotations;

namespace FilmManagement.BL.Domain.Validation;

public static class EnumValidator
{
    public static ValidationResult DefinedValuesOnly(object value, ValidationContext validationContext)
    {
        if (Enum.IsDefined(value.GetType(), value)) return ValidationResult.Success;
        var memberName = validationContext.MemberName;
        var errorMessage = $"The specified value of '{memberName}' is not defined";
        return new ValidationResult(errorMessage, [memberName]);
    }
}