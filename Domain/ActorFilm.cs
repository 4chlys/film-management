using System.ComponentModel.DataAnnotations;

namespace FilmManagement.BL.Domain;

public class ActorFilm
{
    [Required]
    public Actor Actor { get; init; }
    [Required]
    public Film Film { get; init; }
    
    public int? ScreenTime { get; set; }
}