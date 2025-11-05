using System.ComponentModel.DataAnnotations;

namespace FilmManagement.BL.Domain;

[Flags]
public enum Genre
{
    Action = 1,
    Comedy = 2,
    Drama = 4,
    Horror = 8,
    Romance = 16,
    [Display(Name = "Science Fiction")]
    SciFi = 32,
    Thriller = 64,
    Documentary = 128
}