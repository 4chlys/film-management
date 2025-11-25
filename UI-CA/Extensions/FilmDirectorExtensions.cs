namespace FilmManagement.UI.CA.Extensions;

using FilmManagement.BL.Domain;

public static class FilmDirectorExtensions
{
    public static string GetInfo(this Director director)
    {
        if (director.CareerStart.HasValue)
        {
            var yearsActive = DateTime.Now.Year - director.CareerStart.Value;
            return $"{director.Name} from {director.Country}, directing since {director.CareerStart} ({yearsActive} years)";
        }
        return $"{director.Name} from {director.Country}";
    }
}