using FilmManagement.BL.Domain;
using FilmManagement.DAL;

namespace FilmManagement.BL;

public class Manager(IRepository repository) : IManager
{
    public void AddFilm(Film film)
    {
        repository.CreateFilm(film);
    }
    
    public IEnumerable<Film> GetFilms()
    {
        return repository.ReadFilms();
    }

    public void ChangeFilms(IEnumerable<Film> films)
    {
        repository.UpdateFilms(films);
    }
    
    public void RemoveFilm(Film film)
    {
        repository.DeleteFilm(film);
    }
    
    public void AddActor(Actor actor)
    {
        repository.CreateActor(actor);
    }
    
    public IEnumerable<Actor> GetActors()
    {
        return repository.ReadActors();
    }
    
    public void ChangeActors(IEnumerable<Actor> actors)
    {
        repository.UpdateActors(actors);
    }
    public void RemoveActor(Actor actor)
    {
        repository.DeleteActor(actor);
    }
    
    public void AddDirector(FilmDirector director)
    {
        repository.CreateDirector(director);
    }
    
    public IEnumerable<FilmDirector> GetDirectors()
    {
        return repository.ReadDirectors();
    }
    
    public void ChangeDirectors(IEnumerable<FilmDirector> directors)
    {
        repository.UpdateDirectors(directors);
    }
    
    public void RemoveDirector(FilmDirector director)
    {
        repository.DeleteDirector(director);
    }
}