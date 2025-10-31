using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FilmManagement.BL.Domain.Validation;

namespace FilmManagement.BL.Domain;

public class Film
{
    private readonly ICollection<Actor> _actors = [];
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ImdbId { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;
    
    [CustomValidation(typeof(EnumValidator), "DefinedValuesOnly")]
    public Genre Genre { get; set; }
    
    [DateRange(minYear: 1800, mustBePast: true)]
    public DateTime ReleaseDate { get; set; }
    
    [Range(0, 10)]
    public double Rating { get; set; }
    
    [NotMapped]
    public FilmDirector Director { get; set; }
    
    [NotMapped]
    public ICollection<Actor> Actors => _actors;
    
    public void AddActor(Actor actor) => _actors.Add(actor);
}