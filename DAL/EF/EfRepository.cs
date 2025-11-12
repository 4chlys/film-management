using System.Linq.Expressions;
using FilmManagement.BL.Domain;

namespace FilmManagement.DAL.EF;

public class EfRepository(FilmDbContext context) : IRepository
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

    public IEnumerable<Film> ReadFilmsByCriteria(Expression<Func<Film, bool>> predicate)
    {
        return context.Films.Where(predicate).ToList();
    }

    public void UpdateFilms(IEnumerable<Film> films)
    {
        foreach (var film in films)
        {
            context.Films.Update(film);
        }
        context.SaveChanges();  
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

    public IEnumerable<Actor> ReadActorsByCriteria(Expression<Func<Actor, bool>> predicate)
    {
        return context.Actors.Where(predicate).ToList();
    }

    public void UpdateActors(IEnumerable<Actor> actors)
    {
        foreach (var actor in actors)
        {
            context.Actors.Update(actor);
        }
        context.SaveChanges();
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

    public IEnumerable<FilmDirector> ReadAllDirectors()
    {
        return context.Directors.ToList();
    }

    public FilmDirector GetDirectorByName(string name)
    {
        return context.Directors.FirstOrDefault(d => d.Name == name);
    }

    public void UpdateDirectors(IEnumerable<FilmDirector> directors)
    {
        foreach (var director in directors)
        {
            context.Directors.Update(director);
        }
        context.SaveChanges();
    }

    public void DeleteDirector(FilmDirector director)
    {
        context.Directors.Remove(director);
        context.SaveChanges();
    }
}