namespace ConsoleApp.Models;

public class FilmDirector
{
    private readonly List<Film> _films = new();

    public string Name { get; set; }
    public string Country { get; set; } = string.Empty;
    public DateTime CareerStart { get; set; }

    public FilmDirector(string name, DateTime careerStart)
    {
        Name = name;
        CareerStart = careerStart;
    }
    
    public IReadOnlyList<Film> Films => _films.AsReadOnly();
    
    public void AddFilm(Film film) => _films.Add(film);
    public void RemoveFilm(Film film) => _films.Remove(film);
    public void ClearFilms() => _films.Clear();
    
    public void AddFilmsRange(IEnumerable<Film> films) => _films.AddRange(films);

    public override string ToString()
    {
        var filmCount = Films.Count;
        var yearsActive = DateTime.Now.Year - CareerStart.Year;
        return $"{Name} from {Country}, directing since {CareerStart.Year} ({yearsActive} years) - {filmCount} film(s)";
    }
}