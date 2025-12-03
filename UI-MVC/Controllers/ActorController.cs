using FilmManagement.BL;
using Microsoft.AspNetCore.Mvc;

namespace FilmManagement.UI.Web.Controllers;

public class ActorController(IManager manager) : Controller
{
    public IActionResult Details(Guid id)
    {
        var actor = manager.GetActorWithFilms(id);
        if (actor == null)
            return NotFound();
        
        return View(actor);
    }
}