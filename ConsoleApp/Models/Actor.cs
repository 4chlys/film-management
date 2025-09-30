namespace ConsoleApp.Models;

public class Actor
{
    private readonly List<Film> _films = [];

    public string Name { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public int? Age { get; set; }

    public ICollection<Film> Films => _films;
    
    public void AddFilm(Film film) => _films.Add(film);

    public override string ToString()
    {
        int displayAge = Age ?? CalculateAge();
        return $"{Name} from {Nationality}, born {DateOfBirth:yyyy-MMM-dd} (age {displayAge})";
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