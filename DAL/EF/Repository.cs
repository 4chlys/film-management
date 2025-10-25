using FilmManagement.BL.Domain;

namespace FilmManagement.DAL.EF;

public class Repository(FilmDbContext context) : IRepository
{
    public void CreateFilm(Film film)
    {
        context.Films.Add(film);
        context.SaveChanges();
    }
    
    public Film ReadFilm(Guid imdbId)
    {
        return context.Films.Find(imdbId);
    }

    public IEnumerable<Film> ReadAllFilm()
    {
        return context.Films.ToList();
    }

    public IEnumerable<Film> ReadAllFilms()
    {
        return context.Films.ToList();   
    }

    public IEnumerable<Film> ReadFilmsByGenre(Genre genre)
    {
        return context.Films
            .Where(bg => bg.Genre == genre)
            .ToList();
    }

    public void UpdateFilms(IEnumerable<Film> films)
    {
        foreach (var film in films)
        {
            context.Films.Update(film);
            context.SaveChanges();       
        }
    }

    public void DeleteFilm(Film film)
    {
        context.Films.Remove(film);
        context.SaveChanges();       
    }

    public void CreateActor(Actor actor)
    {
        context.Actors.Add(actor);
        context.SaveChanges();
    }

    public Actor ReadActor(Guid imdbId)
    {
        return context.Actors.Find(imdbId);
    }

    public IEnumerable<Actor> ReadAllActors()
    {
        return context.Actors.ToList();   
    }

    public IEnumerable<Actor> ReadActorsByCriteria(string nameFilter, int? minimumAge)
    {
        var query = context.Actors.AsQueryable();
    
        if (!string.IsNullOrWhiteSpace(nameFilter))
        {
            query = query.Where(a => a.Name.ToUpper().Contains(nameFilter.ToUpper()));
        }
    
        if (minimumAge.HasValue)
        {
            query = query.Where(a => a.Age >= minimumAge); // This now queries the DB column!
        }
    
        return query.ToList();
    }

    public void UpdateActors(IEnumerable<Actor> actors)
    {
        foreach (var actor in actors)
        {
            context.Actors.Update(actor);
            context.SaveChanges();
        }
    }

    public void DeleteActor(Actor actor)
    {
        context.Actors.Remove(actor);
        context.SaveChanges();
    }

    public void CreateDirector(FilmDirector director)
    {
        context.Directors.Add(director);
        context.SaveChanges();
    }

    public FilmDirector ReadDirector(Guid imdbId)
    {
        return context.Directors.Find(imdbId);
    }

    public FilmDirector ReadDirectorByName(string name)
    {
        return context.Directors
            .SingleOrDefault(d => d.Name.ToUpper() == name.ToUpper());
    }

    public IEnumerable<FilmDirector> ReadAllDirectors()
    {
        return context.Directors.ToList();
    }

    public void UpdateDirectors(IEnumerable<FilmDirector> directors)
    {
        foreach (var director in directors)
        {
            context.Directors.Update(director);
            context.SaveChanges();
        }
    }

    public void DeleteDirector(FilmDirector director)
    {
        context.Directors.Remove(director);
        context.SaveChanges();
    }
}