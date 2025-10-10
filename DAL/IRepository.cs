using FilmManagement.BL.Domain;

namespace FilmManagement.DAL;

public interface IRepository
{
    void CreateFilm(Film film);
    IEnumerable<Film> ReadFilms();
    void UpdateFilms(IEnumerable<Film> films);
    void DeleteFilm(Film film);
    
    void CreateActor(Actor actor);
    IEnumerable<Actor> ReadActors();
    void UpdateActors(IEnumerable<Actor> actors);
    void DeleteActor(Actor actor);
    
    void CreateDirector(FilmDirector director);   
    IEnumerable<FilmDirector> ReadDirectors();
    void UpdateDirectors(IEnumerable<FilmDirector> directors);
    void DeleteDirector(FilmDirector director);
}