using FilmManagement.BL;
using Microsoft.AspNetCore.Mvc;

namespace FilmManagement.UI.Web.Controllers;

public class DirectorController(IManager manager) : Controller
{
    public IActionResult Details(Guid id)
    {
        var director = manager.GetDirectorWithFilms(id);
        if (director == null)
            return NotFound();
        
        return View(director);
    }
}