using System.ComponentModel.DataAnnotations;

namespace FilmManagement.BL.Domain;

public class FilmDirector : IValidatableObject
{
    private readonly List<Film> _films = [];

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Country must be between 2 and 50 characters")]
    public string Country { get; set; } = string.Empty;
    
    public int? YearStarted { get; set; } 
    
    [Key]
    [StringLength(9, MinimumLength = 9, ErrorMessage = "IMDb ID must be exactly 9 characters")]
    [RegularExpression(@"^nm\d{7,8}$", ErrorMessage = "IMDb ID must start with 'nm' followed by 7-8 digits")]
    public string ImdbId { get; set; } = string.Empty;

    public ICollection<Film> Films => _films;
    
    public void AddFilm(Film film) => _films.Add(film);

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();

        if (YearStarted.HasValue)
        {
            if (YearStarted.Value < 1888)
            {
                errors.Add(new ValidationResult(
                    "Year started must be after 1888 (invention of cinema)!",
                    [nameof(YearStarted)]));
            }
            
            if (YearStarted.Value > DateTime.Now.Year)
            {
                errors.Add(new ValidationResult(
                    "Year started cannot be in the future!",
                    [nameof(YearStarted)]));
            }
        }

        return errors;
    }
}