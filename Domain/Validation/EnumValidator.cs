using System.ComponentModel.DataAnnotations;

namespace FilmManagement.BL.Domain.Validation
{
    public static class EnumValidator
    {
        public static ValidationResult DefinedValuesOnly(object value, ValidationContext context)
        {
            if (value is not Enum enumValue)
            {
                return new ValidationResult(
                    $"{context.MemberName} must be an Enum type. " +
                    $"EnumValidator cannot validate {value?.GetType().Name ?? "null"}.");
            }

            var enumType = enumValue.GetType();
            var isFlagsEnum = enumType.IsDefined(typeof(FlagsAttribute), inherit: false);
            
            var underlyingType = Enum.GetUnderlyingType(enumType);
            var numericValue = Convert.ChangeType(enumValue, underlyingType);

            if (isFlagsEnum)
            {
                var allValidFlags = Enum.GetValuesAsUnderlyingType(enumType)
                    .Cast<object>()
                    .Aggregate(Convert.ChangeType(0, underlyingType), (acc, o) =>
                    {
                        dynamic accumulatedResult = acc;
                        dynamic enumAggregate = o;
                        return accumulatedResult | enumAggregate;
                    });
                
                dynamic val = numericValue;
                dynamic mask = allValidFlags;

                if ((val & ~mask) != 0)
                {
                    return new ValidationResult($"{context.MemberName} contains invalid flag values.");
                }
                /*Start:     acc = 0
                Iteration 1: acc = 0 | 1 (Action)      = 1
                Iteration 2: acc = 1 | 2 (Comedy)      = 3
                Iteration 3: acc = 3 | 4 (Drama)       = 7
                Iteration 4: acc = 7 | 8 (Horror)      = 15
                Iteration 5: acc = 15 | 16 (Romance)   = 31
                Iteration 6: acc = 31 | 32 (SciFi)     = 63
                Iteration 7: acc = 63 | 64 (Thriller)  = 127
                Iteration 8: acc = 127 | 128 (Documentary) = 255

                Final result: 255 (binary: 11111111)*/
            }
            else
            {
                if (!Enum.IsDefined(enumType, value))
                {
                    return new ValidationResult($"{context.MemberName} is not a valid value.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
