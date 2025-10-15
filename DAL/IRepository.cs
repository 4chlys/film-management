using FilmManagement.BL.Domain;

namespace FilmManagement.DAL;

public interface IRepository
{
    void CreateFilm(Film film);
    Film ReadFilm(string imdbId);
    IEnumerable<Film> ReadAllFilms();
    IEnumerable<Film> ReadFilmsByGenre(Genre genre);
    void UpdateFilms(IEnumerable<Film> films);
    void DeleteFilm(Film film);
    
    void CreateActor(Actor actor);
    Actor ReadActor(string imdbId);   
    IEnumerable<Actor> ReadAllActors();
    IEnumerable<Actor> ReadActorsByNamePart(string namePart);
    IEnumerable<Actor> ReadActorsByMinimumAge(int minimumAge);
    void UpdateActors(IEnumerable<Actor> actors);
    void DeleteActor(Actor actor);
    
    void CreateDirector(FilmDirector director); 
    FilmDirector ReadDirector(string imdbId);
    FilmDirector ReadDirectorByName(string name);
    IEnumerable<FilmDirector> ReadAllDirectors();
    void UpdateDirectors(IEnumerable<FilmDirector> directors);
    void DeleteDirector(FilmDirector director);
}