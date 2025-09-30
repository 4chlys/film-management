namespace ConsoleApp.Models;

public class Actor
{
    private readonly List<Film> _films = new();

    public string Name { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public int? Age { get; set; }

    public Actor(string name, DateTime dateOfBirth)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        DateOfBirth = dateOfBirth;
    }
    
    public IReadOnlyList<Film> Films => _films.AsReadOnly();
    
    public void AddFilm(Film film) => _films.Add(film);
    public void RemoveFilm(Film film) => _films.Remove(film);
    public void ClearFilms() => _films.Clear();
    
    public void AddFilmsRange(IEnumerable<Film> films) => _films.AddRange(films);

    public override string ToString()
    {
        int displayAge = Age ?? CalculateAge();
        var filmCount = Films.Count;
        return $"{Name} from {Nationality}, born {DateOfBirth:yyyy-MMM-dd} (age {displayAge}) - {filmCount} film(s)";
    }

    private int CalculateAge()
    {
        var today = DateTime.Today;
        int age = today.Year - DateOfBirth.Year;
        
        if (DateOfBirth.Date > today.AddYears(-age))
            age--;
            
        return age;
    }
}