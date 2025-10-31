using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FilmManagement.BL.Domain;
using FilmManagement.BL.Domain.Validation;

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
    
    [DateRange(minYear:1800, mustBePast:true)]
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
    
    [DateRange(minYear: 1800, mustBePast: true)]
    public DateTime? DateOfDeath { get; set; }
    
    [Range(0, 150)]
    private int _age;
    
    public int Age 
    { 
        get => RefreshAge();
        private set => _age = value;
    }

    [NotMapped]
    public ICollection<Film> Films => _films;
    
    public void AddFilm(Film film) => _films.Add(film);

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();
        if (DateOfDeath.HasValue && DateOfDeath.Value < DateOfBirth)
        {
            errors.Add(new ValidationResult(
                "Date of death cannot be before date of birth!",
                [nameof(DateOfDeath), nameof(DateOfBirth)]));       
        }
        return errors;
    }

    private static int CalculateAge(DateTime dateOfBirth, DateTime? dateOfDeath = null)
    {
        var today = DateTime.Today;
        if (dateOfDeath.HasValue && dateOfDeath.Value < today)
        {
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age))
                age--;
            return age;       
        }
        else
        {
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age))
                age--;
            return age;
        }
    }
    
    private int RefreshAge()
    {
        return CalculateAge(DateOfBirth);
    }
}