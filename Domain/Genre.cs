using System.ComponentModel.DataAnnotations;

namespace FilmManagement.BL.Domain;

public enum Genre
{
    Action = 1,
    Comedy = 2,
    Drama = 3,
    Horror = 4,
    Romance = 5,
    [Display(Name = "Science Fiction")]
    SciFi = 6,
    Thriller = 7,
    Documentary = 8
}