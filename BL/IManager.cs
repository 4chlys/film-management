using FilmManagement.BL.Domain;

namespace FilmManagement.BL;

public interface IManager
{
    void AddFilm(Film film);
    IEnumerable<Film> GetFilms();
    void ChangeFilms(IEnumerable<Film> films);
    void RemoveFilm(Film film);
    
    void AddActor(Actor actor);
    IEnumerable<Actor> GetActors();
    void ChangeActors(IEnumerable<Actor> actors);
    void RemoveActor(Actor actor);
    
    void AddDirector(FilmDirector director);   
    IEnumerable<FilmDirector> GetDirectors();
    void ChangeDirectors(IEnumerable<FilmDirector> directors);
    void RemoveDirector(FilmDirector director);
}