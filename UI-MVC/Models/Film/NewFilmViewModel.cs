using System.ComponentModel.DataAnnotations;
using FilmManagement.BL.Domain;
using FilmManagement.BL.Domain.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FilmManagement.UI.WEB.Models.Film;

public class NewFilmViewModel
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;
    
    [CustomValidation(typeof(EnumValidator), "DefinedValuesOnly")]
    public Genre Genre { get; set; }
    
    [DateRange(minYear: 1800, mustBePast: true)]
    public DateTime ReleaseDate { get; set; }
    
    [Range(0, 10)]
    public double Rating { get; set; }

    public Guid? DirectorId { get; set; }
  
    public List<Guid> SelectedActorIds { get; set; } = new();
    
    public SelectList Directors { get; set; }
    public SelectList Actors { get; set; }
}