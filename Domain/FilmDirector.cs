using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FilmManagement.BL.Domain.Validation;

namespace FilmManagement.BL.Domain;

public class FilmDirector : IValidatableObject
{
    private readonly ICollection<Film> _films = [];

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ImdbId { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(50, MinimumLength = 2)]
    public string Country { get; set; } = string.Empty;
    
    [DateRange(minYear: 1800, mustBePast: true)]
    public int? CareerStart { get; set; } 
    
    [DateRange(minYear: 1800, mustBePast: true)]
    public int? CareerEnd { get; set; }
    
    public ICollection<Film> Films => _films;
    
    public void AddFilm(Film film) => _films.Add(film);

    IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();

        if (CareerStart.HasValue && CareerEnd.HasValue)
        {
            if (CareerStart.Value > CareerEnd.Value)
            {
                errors.Add(new ValidationResult(
                    "Year started cannot be after year ended!",
                    [nameof(CareerStart), nameof(CareerEnd)]));
            }
        }
        return errors;
    }
}