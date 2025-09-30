namespace ConsoleApp.Models;

public class FilmDirector
{
    private readonly List<Film> _films = new();

    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public int YearStarted { get; set; }

    public IReadOnlyList<Film> Films => _films.AsReadOnly();
    
    public void AddFilm(Film film) => _films.Add(film);

    public override string ToString()
    {
        var yearsActive = DateTime.Now.Year - YearStarted;
        return $"{Name} from {Country}, directing since {YearStarted} ({yearsActive} years)";
    }
}