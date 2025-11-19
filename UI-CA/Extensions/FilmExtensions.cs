namespace FilmManagement.UI.CA.Extensions;

using FilmManagement.BL.Domain;

public static class FilmExtensions
{
    public static string GetInfo(this Film film, bool includeActors = true)
    {
        var genres = GetGenreNames(film.Genre);
        var basicInfo = $"{film.Title} ({film.ReleaseDate.Year}) [{genres}] - Rating: {film.Rating:F1} - Directed by {film.Director?.Name ?? "Unknown"}";
        
        if (!includeActors)
            return basicInfo;
        
        if (!film.ActorFilms.Any())
            return $"{basicInfo} - No actors listed";
        
        var actorsInfo = string.Join("\n\t", 
            film.ActorFilms.Select(af => af.Actor.GetInfo(includeFilms: false)));
        
        return $"{basicInfo} - Starring:\n\t{actorsInfo}";
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