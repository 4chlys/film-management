namespace FilmManagement.UI.CA.Extensions;

using FilmManagement.BL.Domain;

public static class FilmExtensions
{
    public static string GetInfo(this Film film)
    {
        var actorNames = string.Join(", ", film.Actors.Select(a => a.Name));
        var actorsText = string.IsNullOrEmpty(actorNames) ? "No actors listed" : $"Starring: {actorNames}";
    
        var genres = GetGenreNames(film.Genre);
    
        return $"{film.Title} ({film.ReleaseDate.Year}) [{genres}] - Rating: {film.Rating:F1} - Directed by {film.Director?.Name ?? "Unknown"} - {actorsText}";
    }

    private static string GetGenreNames(Genre genre)
    {
        var genreList = Enum.GetValues(typeof(Genre))
            .Cast<Genre>()
            .Where(g => g != 0 && genre.HasFlag(g))
            .Select(g => g.GetDisplayName());
    
        return string.Join(", ", genreList);
    }
}