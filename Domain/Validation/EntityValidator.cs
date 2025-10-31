using System.ComponentModel.DataAnnotations;

namespace FilmManagement.BL.Domain.Validation;

public static class EntityValidator
{
    public static void ValidateEntity<T>(T entity)
    {
        var errors = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(entity, 
            new ValidationContext(entity),
            errors, validateAllProperties: true);

        if (isValid) return;
        var errorMessages = errors.Select(e => e.ErrorMessage);
        throw new ArgumentException(string.Join(" | ", errorMessages));
    }
}