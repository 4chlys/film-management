using System.ComponentModel.DataAnnotations;
using FilmManagement.BL.Domain;
using FilmManagement.BL.Domain.Validation;

namespace FilmManagement.UI.WEB.Models.Film;

public class DetailsFilmViewModel
{
    public Guid ImdbId { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Title { get; set; }
    
    [CustomValidation(typeof(EnumValidator), "DefinedValuesOnly")]
    public Genre Genre { get; set; }
    
    [DateRange(minYear: 1800, mustBePast: true)]
    public DateTime ReleaseDate { get; set; }
    
    [Range(0, 10)]
    public double Rating { get; set; }
}