namespace FilmManagement.UI.CA.Utilities;

public static class ValidationHelper
{
    public static void ShowValidationErrors(string errorMessage)
    {
        var errors = errorMessage.Split('|', StringSplitOptions.RemoveEmptyEntries);
        Console.WriteLine("\nValidation errors:");
        foreach (var error in errors)
        {
            Console.WriteLine($"\t- {error.Trim()}");
        }
    }

    public static void ShowSuccessMessage(string message)
    {
        Console.WriteLine($"\nSuccess:{message}");
    }

    public static void ShowErrorMessage(string message)
    {
        Console.WriteLine($"\nError: {message}");
    }
}