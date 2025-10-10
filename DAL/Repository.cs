using FilmManagement.BL.Domain;

namespace FilmManagement.DAL;

public class Repository : IRepository
{
    public void CreateFilm(Film film)
    {
        InMemoryStorage.Films.Add(film);
    }
    
    public IEnumerable<Film> ReadFilms()
    {
        return InMemoryStorage.Films;
    }

    public void UpdateFilms(IEnumerable<Film> films)
    {
        InMemoryStorage.Films.AddRange(films);
    }
    
    public void DeleteFilm(Film film)
    {
        InMemoryStorage.Films.Remove(film);
    }
    
    public void CreateActor(Actor actor)
    {
        InMemoryStorage.Actors.Add(actor);
    }

    public IEnumerable<Actor> ReadActors()
    {
        return InMemoryStorage.Actors;
    }

    public void UpdateActors(IEnumerable<Actor> actors)
    {
        InMemoryStorage.Actors.AddRange(actors);
    }

    public void DeleteActor(Actor actor)
    {
        InMemoryStorage.Actors.Remove(actor);
    }
    
    public void CreateDirector(FilmDirector director)
    {
        InMemoryStorage.FilmDirectors.Add(director);       
    }

    public IEnumerable<FilmDirector> ReadDirectors()
    {
        return InMemoryStorage.FilmDirectors;
    }

    public void UpdateDirectors(IEnumerable<FilmDirector> directors)
    {
        InMemoryStorage.FilmDirectors.AddRange(directors);       
    }
    
    public void DeleteDirector(FilmDirector director)
    {
        InMemoryStorage.FilmDirectors.Remove(director);       
    }
}