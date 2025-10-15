namespace FilmManagement.UI.CA.Extensions;

using FilmManagement.BL.Domain;

public static class FilmExtensions
{
    public static string GetInfo(this Film film)
    {
        var actorNames = string.Join(", ", film.Actors.Select(a => a.Name));
        var actorsText = string.IsNullOrEmpty(actorNames) ? "No actors listed" : $"Starring: {actorNames}";
        return $"{film.Title} ({film.ReleaseDate.Year}) [{film.Genre}] - Rating: {film.Rating:F1} - Directed by {film.Director?.Name ?? "Unknown"} - {actorsText}";
    }
}