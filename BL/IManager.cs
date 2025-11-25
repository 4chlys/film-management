using System.ComponentModel.DataAnnotations;
using FilmManagement.BL.Domain;
using FilmManagement.DAL;

namespace FilmManagement.BL;

public interface IManager
{
    Film AddFilm(string title, Genre genre, DateTime releaseDate, double rating, Director director);
    void AddActorToFilm(Guid filmId, Guid actorId, int? screenTime);
    Film GetFilm(Guid imdbId);   
    IEnumerable<Film> GetAllFilms();
    IEnumerable<Film> GetAllFilmsWithActorsAndDirectors();
    IEnumerable<Film> GetFilmsByGenre(Genre genre); 
    void ChangeFilms(IEnumerable<Film> films);
    void RemoveFilm(Film film);
    void RemoveActorFromFilm(Guid filmId, Guid actorId);
    
    Actor AddActor(string name, string nationality, DateTime dateOfBirth, DateTime? dateOfDeath);
    Actor GetActor(Guid imdbId);  
    IEnumerable<Actor> GetAllActors();
    IEnumerable<Actor> GetAllActorsWithFilms();
    IEnumerable<Actor> GetActorsByCriteria(string nameFilter, int? minimumAge);
    IEnumerable<Actor> GetActorsOfFilm(Guid imdbId);
    void ChangeActors(IEnumerable<Actor> actors);
    void RemoveActor(Actor actor);
    
    Director AddDirector(string name, string country, int? yearStarted, int? yearEnded); 
    Director GetDirector(Guid imdbId);
    Director GetDirectorByName(string name);
    IEnumerable<Director> GetAllDirectors();
    IEnumerable<Director> GetAllDirectorsWithFilms();
    void ChangeDirectors(IEnumerable<Director> directors);
    void RemoveDirector(Director director);
    
    IEnumerable<Genre> GetAllGenres();
}