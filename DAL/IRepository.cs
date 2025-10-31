using FilmManagement.BL.Domain;

namespace FilmManagement.DAL;

public interface IRepository
{
    void CreateFilm(Film film);
    Film ReadFilm(Guid imdbId);
    IEnumerable<Film> ReadAllFilms();
    IEnumerable<Film> ReadFilmsByGenre(Genre genre);
    void UpdateFilms(IEnumerable<Film> films);
    void DeleteFilm(Film film);
    
    void CreateActor(Actor actor);
    Actor ReadActor(Guid imdbId);   
    IEnumerable<Actor> ReadAllActors();
    public IEnumerable<Actor> ReadActorsByCriteria(string nameFilter, int? minimumAge);
    void UpdateActors(IEnumerable<Actor> actors);
    void DeleteActor(Actor actor);
    
    void CreateDirector(FilmDirector director); 
    FilmDirector ReadDirector(Guid imdbId);
    FilmDirector ReadDirectorByName(string name);
    IEnumerable<FilmDirector> ReadAllDirectors();
    void UpdateDirectors(IEnumerable<FilmDirector> directors);
    void DeleteDirector(FilmDirector director);
}
