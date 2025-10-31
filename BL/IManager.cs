using System.ComponentModel.DataAnnotations;
using FilmManagement.BL.Domain;
using FilmManagement.DAL;

namespace FilmManagement.BL;

public interface IManager
{
    Film AddFilm(string title, Genre genre, DateTime releaseDate, double rating, FilmDirector director);
    Film GetFilm(Guid imdbId);   
    IEnumerable<Film> GetFilms();
    IEnumerable<Film> GetFilmsByGenre(Genre genre); 
    void ChangeFilms(IEnumerable<Film> films);
    void RemoveFilm(Film film);
    
    Actor AddActor(string name, string nationality, DateTime dateOfBirth, DateTime? dateOfDeath);
    Actor GetActor(Guid imdbId);  
    IEnumerable<Actor> GetActors();
    IEnumerable<Actor> GetActorsByCriteria(string nameFilter, int? minimumAge);
    void ChangeActors(IEnumerable<Actor> actors);
    void RemoveActor(Actor actor);
    
    FilmDirector AddDirector(string name, string country, int? yearStarted, int? yearEnded); 
    FilmDirector GetDirector(Guid imdbId);
    FilmDirector GetDirectorByName(string name);
    IEnumerable<FilmDirector> GetDirectors();
    void ChangeDirectors(IEnumerable<FilmDirector> directors);
    void RemoveDirector(FilmDirector director);
    
    IEnumerable<Genre> GetAllGenres();
}