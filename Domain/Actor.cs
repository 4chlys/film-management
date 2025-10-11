using System.ComponentModel.DataAnnotations;

namespace FilmManagement.BL.Domain;

public class Actor : IValidatableObject
{
    private readonly List<Film> _films = [];

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Nationality is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Nationality must be between 2 and 50 characters")]
    public string Nationality { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Date of birth is required")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }
    
    [Range(0, 150, ErrorMessage = "Age must be between 0 and 150")]
    public int? Age { get; set; }
    
    [Key]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "IMDb ID must be exactly 10 characters")]
    [RegularExpression(@"^nm\d{7,8}$", ErrorMessage = "IMDb ID must start with 'nm' followed by 7-8 digits")]
    public string ImdbId { get; set; } = string.Empty;

    public ICollection<Film> Films => _films;
    
    public void AddFilm(Film film) => _films.Add(film);

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();
        
        if (DateOfBirth >= DateTime.Now)
        {
            errors.Add(new ValidationResult(
                "Date of birth should be in the past!",
                new[] { nameof(DateOfBirth) }));
        }
        
        if (DateOfBirth.Year < 1800)
        {
            errors.Add(new ValidationResult(
                "Date of birth must be after 1800!",
                new[] { nameof(DateOfBirth) }));
        }
        
        if (Age.HasValue)
        {
            int calculatedAge = CalculateAge();
            if (Math.Abs(Age.Value - calculatedAge) > 1)
            {
                errors.Add(new ValidationResult(
                    $"Provided age ({Age}) doesn't match date of birth (calculated age: {calculatedAge})!",
                    new[] { nameof(Age) }));
            }
        }

        return errors;
    }

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