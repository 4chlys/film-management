namespace ConsoleApp.Models;

public class Actor
{
    private string _name = string.Empty;
    private string _nationality = string.Empty;
    private DateTime _dateOfBirth;
    private int? _age; // nullable integer
    private List<Film> _films = new List<Film>();

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public string Nationality
    {
        get { return _nationality; }
        set { _nationality = value; }
    }

    public DateTime DateOfBirth
    {
        get { return _dateOfBirth; }
        set { _dateOfBirth = value; }
    }

    public int? Age
    {
        get { return _age; }
        set { _age = value; }
    }

    public List<Film> Films
    {
        get { return _films; }
        set { _films = value; }
    }

    public override string ToString()
    {
        int displayAge;
        if (Age.HasValue)
        {
            displayAge = Age.Value;
        }
        else
        {
            var today = DateTime.Today;
            displayAge = today.Year - DateOfBirth.Year;
            
            if (DateOfBirth.Date > today.AddYears(-displayAge))
                displayAge--;
        }

        var filmCount = Films.Count;
        return $"{Name} from {Nationality}, born {DateOfBirth:yyyy-MMM-dd} (age {displayAge}) - {filmCount} film(s)";
    }
}
