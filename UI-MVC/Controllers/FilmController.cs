using FilmManagement.BL;
using Microsoft.AspNetCore.Mvc;

namespace FilmManagement.UI.Web.Controllers;

public class FilmController(IManager filmManager) : Controller
{
    public IActionResult Index()
    {
        var allFilms = filmManager.GetAllFilms();
        return View(allFilms);
    }
}