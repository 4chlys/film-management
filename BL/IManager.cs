using FilmManagement.BL.Domain;

namespace FilmManagement.BL;

public interface IManager
{
    Film AddFilm(string title, Genre genre, DateTime releaseDate, double rating, FilmDirector director);
    Film GetFilm(string imdbId);   
    IEnumerable<Film> GetFilms();
    IEnumerable<Film> GetFilmsByGenre(Genre genre); 
    void ChangeFilms(IEnumerable<Film> films);
    void RemoveFilm(Film film);
    
    Actor AddActor(string name, string nationality, DateTime dateOfBirth, int? age);
    Actor GetActor(string imdbId);  
    IEnumerable<Actor> GetActors();
    IEnumerable<Actor> GetActorsByNationality(string nationality);
    IEnumerable<Actor> GetActorsByAge(int age);
    void ChangeActors(IEnumerable<Actor> actors);
    void RemoveActor(Actor actor);
    
    FilmDirector AddDirector(string name, string country, int? yearStarted); 
    FilmDirector GetDirector(string imdbId); 
    IEnumerable<FilmDirector> GetDirectors();
    void ChangeDirectors(IEnumerable<FilmDirector> directors);
    void RemoveDirector(FilmDirector director);
}