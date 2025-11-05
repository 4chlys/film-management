using FilmManagement.BL.Domain;

namespace FilmManagement.DAL;

public interface IRepository
{
    void CreateFilm(Film film);
    Film ReadFilm(Guid imdbId);
    IEnumerable<Film> ReadAllFilms();
    IQueryable<Film> ReadAllFilmsQueryable();
    void UpdateFilms(IEnumerable<Film> films);
    void DeleteFilm(Film film);
    
    void CreateActor(Actor actor);
    Actor ReadActor(Guid imdbId);  
    IEnumerable<Actor> ReadAllActors();
    IQueryable<Actor> ReadAllActorsQueryable();
    void UpdateActors(IEnumerable<Actor> actors);
    void DeleteActor(Actor actor);
    
    void CreateDirector(FilmDirector director); 
    FilmDirector ReadDirector(Guid imdbId);
    IEnumerable<FilmDirector> ReadAllDirectors();
    IQueryable<FilmDirector> ReadAllDirectorsQueryable();
    void UpdateDirectors(IEnumerable<FilmDirector> directors);
    void DeleteDirector(FilmDirector director);
}
