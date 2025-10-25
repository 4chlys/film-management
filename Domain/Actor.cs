using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FilmManagement.BL.Domain;

public class Actor : IValidatableObject
{
    private readonly ICollection<Film> _films = [];

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ImdbId { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(50, MinimumLength = 2)]
    public string Nationality { get; set; } = string.Empty;
    
    private DateTime _dateOfBirth;
    
    public DateTime DateOfBirth 
    { 
        get => _dateOfBirth;
        set
        {
            _dateOfBirth = value;
            Age = CalculateAge(value);
        }
    }
    
    [Range(0, 150)]
    public int Age { get; private set; }

    [NotMapped]
    public ICollection<Film> Films => _films;
    
    public void AddFilm(Film film) => _films.Add(film);

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();
        
        if (DateOfBirth >= DateTime.Now)
        {
            errors.Add(new ValidationResult(
                "Date of birth should be in the past!",
                [nameof(DateOfBirth)]));
        }
        
        if (DateOfBirth.Year < 1800)
        {
            errors.Add(new ValidationResult(
                "Date of birth must be after 1800!",
                [nameof(DateOfBirth)]));
        }

        return errors;
    }

    private static int CalculateAge(DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        int age = today.Year - dateOfBirth.Year;
        if (dateOfBirth.Date > today.AddYears(-age))
            age--;
        return age;
    }
    
    public void RefreshAge()
    {
        Age = CalculateAge(DateOfBirth);
    }
}