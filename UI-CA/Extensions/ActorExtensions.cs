namespace FilmManagement.UI.CA.Extensions;

using FilmManagement.BL.Domain;

public static class ActorExtensions
{
    public static string GetInfo(this Actor actor, bool includeFilms = true)
    {
        var ageText = actor.DateOfDeath.HasValue 
            ? $"age {actor.Age} at death" 
            : $"age {actor.Age}";
        
        var basicInfo = $"{actor.Name}, {actor.Nationality}, born {actor.DateOfBirth:yyyy-MMM-dd} ({ageText})";
        
        if (!includeFilms)
            return basicInfo;
        
        if (!actor.ActorFilms.Any())
            return $"{basicInfo} - No films listed";
        
        var filmsInfo = string.Join("\n\t", 
            actor.ActorFilms.Select(af => af.Film.GetInfo(includeActors: false)));
        
        return $"{basicInfo} - Starring in:\n\t{filmsInfo}";
    }
}