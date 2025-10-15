using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FilmManagement.UI.CA.Extensions;

public static class EnumExtensions
{
    public static T? ParseEnum<T>(this string value, bool ignoreCase = true) where T : struct, Enum
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        return Enum.TryParse<T>(value.Trim(), ignoreCase, out var result) ? result : null;
    }
    
    public static string GetDisplayName(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var displayAttribute = field?
            .GetCustomAttribute<DisplayAttribute>();

        return displayAttribute?.Name ?? value.ToString();
    }
    
    public static IEnumerable<string> GetAllDisplayNames<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T))
            .Cast<Enum>()
            .Select(e => e.GetDisplayName());
    }
}