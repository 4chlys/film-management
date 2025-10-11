using System.ComponentModel.DataAnnotations;

namespace FilmManagement.BL.Domain;

public class Film : IValidatableObject
{
    private readonly List<Actor> _actors = [];
    
    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters")]
    public string Title { get; set; } = string.Empty;
    
    public Genre Genre { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
    
    [Range(0, 10, ErrorMessage = "Rating must be between 0 and 10")]
    public double Rating { get; set; }
    
    public FilmDirector Director { get; set; }
    
    [Key]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "IMDb ID must be exactly 10 characters")]
    [RegularExpression(@"^tt\d{7,8}$", ErrorMessage = "IMDb ID must start with 'tt' followed by 7-8 digits")]
    public string ImdbId { get; set; } = string.Empty;
    
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
    
    public override string ToString()
    {
        var actorNames = string.Join(", ", Actors.Select(a => a.Name));
        var actorsText = string.IsNullOrEmpty(actorNames) ? "No actors listed" : $"Starring: {actorNames}";
        return $"{Title} ({ReleaseDate.Year}) [{Genre}] - Rating: {Rating:F1} - Directed by {Director?.Name ?? "Unknown"} - {actorsText}";
    }
}