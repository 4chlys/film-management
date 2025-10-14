namespace FilmManagement.UI.CA.Extensions;

using FilmManagement.BL.Domain;

public static class FilmDirectorExtension
{
    public static string GetInfo(this FilmDirector director)
    {
        if (director.YearStarted.HasValue)
        {
            var yearsActive = DateTime.Now.Year - director.YearStarted.Value;
            return $"{director.Name} from {director.Country}, directing since {director.YearStarted} ({yearsActive} years)";
        }
        return $"{director.Name} from {director.Country}";
    }
}