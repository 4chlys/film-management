using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmManagement.BL.Domain;

public class Film : IValidatableObject
{
    private readonly ICollection<Actor> _actors = [];
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ImdbId { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;
    
    public Genre Genre { get; set; }
    
    public DateTime ReleaseDate { get; set; }
    
    [Range(0, 10)]
    public double Rating { get; set; }
    
    [NotMapped]
    public FilmDirector Director { get; set; }
    
    [NotMapped]
    public ICollection<Actor> Actors => _actors;
    
    public void AddActor(Actor actor) => _actors.Add(actor);

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();
        
        if (!Enum.IsDefined(typeof(Genre), Genre))
        {
            errors.Add(new ValidationResult(
                "Genre unknown!",
                [nameof(Genre)]));
        }
        
        if (ReleaseDate > DateTime.Now)
        {
            errors.Add(new ValidationResult(
                "Release date cannot be in the future!",
                [nameof(ReleaseDate)]));
        }
        
        if (ReleaseDate.Year < 1888)
        {
            errors.Add(new ValidationResult(
                "Release date must be after 1888 (when cinema was invented)!",
                [nameof(ReleaseDate)]));
        }
        
        if (Math.Round(Rating, 1) != Rating)
        {
            errors.Add(new ValidationResult(
                "Rating can have at most one decimal place!",
                [nameof(Rating)]));
        }

        return errors;
    }
}