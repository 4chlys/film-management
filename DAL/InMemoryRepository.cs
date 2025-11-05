using FilmManagement.BL.Domain;
using FilmManagement.DAL.EF;

namespace FilmManagement.DAL;

public class InMemoryRepository : IRepository
{
    //Film operations   
    public void CreateFilm(Film film)
    {
        film.ImdbId = Guid.NewGuid();
        InMemoryStorage.Films.Add(film);
    }

    public Film ReadFilm(Guid imdbId)
    {
        return InMemoryStorage.Films.SingleOrDefault(f => f.ImdbId == imdbId);       
    }

    public IEnumerable<Film> ReadAllFilms()
    {
        return InMemoryStorage.Films;
    }

    public IQueryable<Film> ReadAllFilmsQueryable()
    {
        return InMemoryStorage.Films.AsQueryable();
    }

    public void UpdateFilms(IEnumerable<Film> films)
    {
        foreach (var film in films)
        {
            var existing = InMemoryStorage.Films.SingleOrDefault(f => f.ImdbId == film.ImdbId);
            if (existing == null)
            {
                CreateFilm(film);
                continue;
            }
            existing.Title = film.Title;
            existing.Genre = film.Genre;
            existing.ReleaseDate = film.ReleaseDate;
            existing.Rating = film.Rating;
            existing.Director = film.Director;
        }
    }
    
    public void DeleteFilm(Film film)
    {
        InMemoryStorage.Films.Remove(film);
    }
    
    //Actor operations  
    public void CreateActor(Actor actor)
    {
        actor.ImdbId = Guid.NewGuid();
        InMemoryStorage.Actors.Add(actor);
    }

    public Actor ReadActor(Guid imdbId)
    {
        return InMemoryStorage.Actors.SingleOrDefault(a => a.ImdbId == imdbId);       
    }

    public IEnumerable<Actor> ReadAllActors()
    {
        return InMemoryStorage.Actors;
    }

    public IQueryable<Actor> ReadAllActorsQueryable()
    {
        return InMemoryStorage.Actors.AsQueryable();
    }

    public void UpdateActors(IEnumerable<Actor> actors)
    {
        foreach (var actor in actors)
        {
            var existing = InMemoryStorage.Actors.SingleOrDefault(a => a.ImdbId == actor.ImdbId);
            if (existing == null)
            {
                CreateActor(actor);
                continue;
            }
            existing.Name = actor.Name;
            existing.Nationality = actor.Nationality;
            existing.DateOfBirth = actor.DateOfBirth;
        }
    }

    public void DeleteActor(Actor actor)
    {
        InMemoryStorage.Actors.Remove(actor);
    }
    
    //Director operations
    public void CreateDirector(FilmDirector director)
    {
        director.ImdbId = Guid.NewGuid();
        InMemoryStorage.FilmDirectors.Add(director);
    }

    public FilmDirector ReadDirector(Guid imdbId)
    {
        return InMemoryStorage.FilmDirectors.SingleOrDefault(d => d.ImdbId == imdbId);       
    }

    public IEnumerable<FilmDirector> ReadAllDirectors()
    {
        return InMemoryStorage.FilmDirectors;
    }

    public IQueryable<FilmDirector> ReadAllDirectorsQueryable()
    {
        return InMemoryStorage.FilmDirectors.AsQueryable();
    }

    public void UpdateDirectors(IEnumerable<FilmDirector> directors)
    {
        foreach (var director in directors)
        {
            var existing = InMemoryStorage.FilmDirectors.FirstOrDefault(d => d.ImdbId == director.ImdbId);
            if (existing == null)
            {
                CreateDirector(director);
                continue;
            }
            existing.Name = director.Name;
            existing.Country = director.Country;
            existing.YearStarted = director.YearStarted;
        }
    }
    
    public void DeleteDirector(FilmDirector director)
    {
        InMemoryStorage.FilmDirectors.Remove(director);       
    }
}