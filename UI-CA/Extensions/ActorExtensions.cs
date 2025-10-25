namespace FilmManagement.UI.CA.Extensions;

using FilmManagement.BL.Domain;

public static class ActorExtensions
{
    public static string GetInfo(this Actor actor)
    {
        return $"{actor.Name}, {actor.Nationality}, born {actor.DateOfBirth:yyyy-MMM-dd} (age {actor.Age})";
    }
}