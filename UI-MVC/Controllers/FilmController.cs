using System.Collections;
using FilmManagement.BL;
using FilmManagement.BL.Domain;
using FilmManagement.UI.WEB.Models.Film;
using Microsoft.AspNetCore.Mvc;

namespace FilmManagement.UI.Web.Controllers;

public class FilmController(IManager filmManager) : Controller
{
    // GET
    public IActionResult Index()
    {
        var allFilms = filmManager.GetAllFilms();
        return View(allFilms);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(NewFilmViewModel newFilm)
    {
        if (!ModelState.IsValid)
            return View(newFilm);

        var addedFilm = filmManager.AddFilm(newFilm.Title, newFilm.Genre, newFilm.ReleaseDate, newFilm.Rating, null);
        return RedirectToAction("Index");
    }

    public IActionResult Details(Guid id)
    {
        var film = filmManager.GetFilm(id);
        return View(film);
    }

    [HttpGet]
    public IActionResult Edit(Guid id)
    {
        var film = filmManager.GetFilm(id);
        
        var editFilm = new EditFilmViewModel()
        {
            ImdbId = film.ImdbId,
            Title = film.Title,
            Genre = film.Genre,
            ReleaseDate = film.ReleaseDate,
            Rating = film.Rating
        };
        return View(editFilm);
    }

    [HttpPost]
    public IActionResult Edit(Guid id, EditFilmViewModel editFilm)
    {
        if (!ModelState.IsValid)
            return View(editFilm);
        
        var editExistingFilm = filmManager.GetFilm(id);
        
        editExistingFilm.Title = editFilm.Title;
        editExistingFilm.Genre = editFilm.Genre;
        editExistingFilm.ReleaseDate = editFilm.ReleaseDate;
        editExistingFilm.Rating = editFilm.Rating;
    
        IEnumerable<Film> editFilms = new List<Film> { editExistingFilm };
        filmManager.ChangeFilm(TODO);
        
        return RedirectToAction("Details", new { Id = editExistingFilm.ImdbId });
    }

    [HttpGet]
    public IActionResult Delete(Guid id)
    {
        var film = filmManager.GetFilm(id);

        var deleteFilm = new DeleteFilmViewModel()
        {
            ImdbId = film.ImdbId,
            Title = film.Title
        };
        return View(deleteFilm);
    }

    [HttpPost]
    public IActionResult Delete(Guid id, DeleteFilmViewModel deleteFilm)
    {
        if (!ModelState.IsValid)
            return View(deleteFilm);
        
        filmManager.RemoveFilm(filmManager.GetFilm(id));
        return RedirectToAction("Index");
    }
}