namespace FilmManagement.UI.CA.Extensions;

using FilmManagement.BL.Domain;

public static class ActorExtension
{
    public static string GetInfo(this Actor actor)
    {
        int displayAge = actor.Age ?? CalculateAge(actor);
        return $"{actor.Name}, {actor.Nationality}, born {actor.DateOfBirth:yyyy-MMM-dd} (age {displayAge})";
    }

    private static int CalculateAge(Actor actor)
    {
        var today = DateTime.Today;
        int age = today.Year - actor.DateOfBirth.Year;
        if (actor.DateOfBirth.Date > today.AddYears(-age))
            age--;
        return age;
    }
}