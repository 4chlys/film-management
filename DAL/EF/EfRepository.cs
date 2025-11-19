using System.Linq.Expressions;
using FilmManagement.BL.Domain;
using Microsoft.EntityFrameworkCore;

namespace FilmManagement.DAL.EF;

public class EfRepository(FilmDbContext context) : IRepository
{
    public void CreateFilm(Film film)
    {
        context.Films.Add(film);
        context.SaveChanges();
    }

    public void CreateActorFilm(ActorFilm actorFilm)
    {
        var film = context.Films.Find(actorFilm.Film.ImdbId);
        if (film == null)
        {
            throw new Exception("Film not found");
        }
        film.ActorFilms.Add(actorFilm);
        context.SaveChanges();
    }

    public Film ReadFilm(Guid imdbId)
    {
        return context.Films
            .Include(f => f.Director)
            .Include(f => f.ActorFilms)
                .ThenInclude(af => af.Actor)
            .SingleOrDefault(f => f.ImdbId == imdbId);
    }

    public IEnumerable<Film> ReadAllFilms()
    {
        return context.Films.ToList();
    }

    public IEnumerable<Film> ReadAllFilmsWithActorsAndDirectors()
    {
        return context.Films
            .Include(f => f.Director)
            .Include(f => f.ActorFilms)
                .ThenInclude(af => af.Actor)
            .ToList();   
    }

    public IEnumerable<Film> ReadFilmsByCriteria(Expression<Func<Film, bool>> predicate)
    {
        return context.Films
            .Include(f => f.Director)
            .Include(f => f.ActorFilms)
                .ThenInclude(af => af.Actor)
            .Where(predicate)
            .ToList();
    }

    public IEnumerable<Actor> ReadActorsOfFilm(Guid imdbId)
    {
        return context.Films
            .Include(f => f.ActorFilms)
                .ThenInclude(af => af.Actor)
            .SingleOrDefault(f => f.ImdbId == imdbId)?
            .ActorFilms.Select(af => af.Actor);
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

    public void DeleteActorFilm(ActorFilm actorFilm)
    {
        var film = context.Films.Find(actorFilm.Film.ImdbId);
        if (film == null)
        {
            throw new Exception("Film not found");
        }
        film.ActorFilms.Remove(actorFilm);
        context.SaveChanges();
    }

    public void CreateActor(Actor actor)
    {
        context.Actors.Add(actor);
        context.SaveChanges();
    }

    public Actor ReadActor(Guid imdbId)
    {
        return context.Actors
            .Include(a => a.ActorFilms)
                .ThenInclude(af => af.Film)
                    .ThenInclude(f => f.Director)
            .SingleOrDefault(a => a.ImdbId == imdbId);
    }

    public IEnumerable<Actor> ReadAllActors()
    {
        return context.Actors.ToList();
    }

    public IEnumerable<Actor> ReadAllActorsWithFilms()
    {
        return context.Actors
            .Include(a => a.ActorFilms)
                .ThenInclude(af => af.Film)
            .ToList();
    }

    public IEnumerable<Actor> ReadActorsByCriteria(Expression<Func<Actor, bool>> predicate)
    {
        return context.Actors
            .Include(a => a.ActorFilms)
                .ThenInclude(af => af.Film)
            .Where(predicate)
            .ToList();
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
        return context.Directors
            .Include(d => d.Films)
            .SingleOrDefault(d => d.ImdbId == imdbId);
    }
    
    public IEnumerable<FilmDirector> ReadAllDirectors()
    {
        return context.Directors.ToList();
    }

    public IEnumerable<FilmDirector> ReadAllDirectorsWithFilms()
    {
        return context.Directors
            .Include(d => d.Films)
            .ToList();
    }

    public FilmDirector ReadDirectorByName(string name)
    {
        return context.Directors
            .Include(d => d.Films)
            .FirstOrDefault(d => d.Name == name);
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