using System.ComponentModel.DataAnnotations;

namespace FilmManagement.UI.Web.Models.Film;

public class DeleteFilmViewModel
{
    public Guid ImdbId { get; set; }
    
    [Display(Name = "Film Title")]
    public string Title { get; set; } = string.Empty;
    
    [Display(Name = "Director")]
    public string DirectorName { get; set; } = string.Empty;
    
    [Display(Name = "Release Year")]
    public int ReleaseYear { get; set; }
}