using System.Linq.Expressions;
using FilmManagement.BL.Domain;

namespace FilmManagement.DAL;

public interface IRepository
{
    void CreateFilm(Film film);
    void CreateActorFilm(ActorFilm actorFilm);
    Film ReadFilm(Guid imdbId);
    IEnumerable<Film> ReadAllFilms();
    IEnumerable<Film> ReadAllFilmsWithActorsAndDirectors();
    IEnumerable<Film> ReadFilmsByCriteria(Expression<Func<Film, bool>> predicate);
    IEnumerable<Actor> ReadActorsOfFilm(Guid imdbId);
    void UpdateFilms(IEnumerable<Film> films);
    void DeleteFilm(Film film);
    void DeleteActorFilm(ActorFilm actorFilm);
    
    void CreateActor(Actor actor);
    Actor ReadActor(Guid imdbId);
    IEnumerable<Actor> ReadAllActors();
    IEnumerable<Actor> ReadAllActorsWithFilms();
    IEnumerable<Actor> ReadActorsByCriteria(Expression<Func<Actor, bool>> predicate);
    void UpdateActors(IEnumerable<Actor> actors);
    void DeleteActor(Actor actor);
    
    void CreateDirector(FilmDirector director); 
    FilmDirector ReadDirector(Guid imdbId);
    FilmDirector ReadDirectorByName(string name);
    IEnumerable<FilmDirector> ReadAllDirectors();
    IEnumerable<FilmDirector> ReadAllDirectorsWithFilms();
    void UpdateDirectors(IEnumerable<FilmDirector> directors);
    void DeleteDirector(FilmDirector director);
}