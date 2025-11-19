using FilmManagement.BL.Domain;
using FilmManagement.UI.CA.Extensions;

namespace FilmManagement.UI.CA.Utilities;

public static class InputParser
{
    public static bool TryParseGuid(string input, out Guid result, string fieldName = "ID")
    {
        if (Guid.TryParse(input, out result))
            return true;
        
        Console.WriteLine($"Error: Invalid {fieldName} format.");
        return false;
    }

    public static bool TryParseDateTime(string input, out DateTime result, string fieldName = "date")
    {
        if (DateTime.TryParse(input, out result))
            return true;
        
        Console.WriteLine($"Error: Invalid {fieldName} format. Please use yyyy-MM-dd.");
        return false;
    }

    public static bool TryParseDouble(string input, out double result, string fieldName = "number")
    {
        if (double.TryParse(input, out result))
            return true;
        
        Console.WriteLine($"Error: Invalid {fieldName} format.");
        return false;
    }

    public static bool TryParseInt(string input, out int result, string fieldName = "number")
    {
        if (int.TryParse(input, out result))
            return true;
        
        Console.WriteLine($"Error: Invalid {fieldName} format.");
        return false;
    }

    public static DateTime? ParseOptionalDateTime(string input, string fieldName = "date")
    {
        if (string.IsNullOrWhiteSpace(input))
            return null;

        if (TryParseDateTime(input, out DateTime result, fieldName))
            return result;
        
        return null;
    }

    public static int? ParseOptionalInt(string input, string fieldName = "number")
    {
        if (string.IsNullOrWhiteSpace(input))
            return null;

        if (TryParseInt(input, out int result, fieldName))
            return result;
        
        return null;
    }

    public static Genre ParseGenreFlags(string input)
    {
        var genres = input.ParseEnumFlags<Genre>();
        
        if (genres == 0)
        {
            Console.WriteLine("Error: Invalid genre(s). Please try again.");
            return 0;
        }
        
        return genres;
    }

    public static string PromptForInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? "";
    }

    public static Genre PromptForGenres(string prompt)
    {
        Genre genres = 0;
        while (genres == 0)
        {
            Console.Write($"{prompt} ({EnumExtensions.GetEnumOptionsPrompt<Genre>()}): ");
            string input = Console.ReadLine() ?? "";
            genres = ParseGenreFlags(input);
        }
        return genres;
    }
}