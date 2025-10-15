using FilmManagement.BL.Domain;

namespace FilmManagement.DAL;

public class Repository : IRepository
{
    //Film operations   
    public void CreateFilm(Film film)
    {
        if (string.IsNullOrEmpty(film.ImdbId))
        {
            film.ImdbId = ImdbIdGenerator.GenerateFilmId();
        }
        InMemoryStorage.Films.Add(film);
    }

    public Film ReadFilm(string imdbId)
    {
        return InMemoryStorage.Films.FirstOrDefault(f => f.ImdbId == imdbId);       
    }

    public IEnumerable<Film> ReadAllFilms()
    {
        return InMemoryStorage.Films;
    }

    public IEnumerable<Film> ReadFilmsByGenre(Genre genre)
    {
        return InMemoryStorage.Films.Where(f => f.Genre == genre);       
    }

    public void UpdateFilms(IEnumerable<Film> films)
    {
        foreach (var film in films)
        {
            var existing = InMemoryStorage.Films.FirstOrDefault(f => f.ImdbId == film.ImdbId);
            if (existing == null) continue;
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
        if (string.IsNullOrEmpty(actor.ImdbId))
        {
            actor.ImdbId = ImdbIdGenerator.GenerateActorId();
        }
        InMemoryStorage.Actors.Add(actor);
    }

    public Actor ReadActor(string imdbId)
    {
        return InMemoryStorage.Actors.FirstOrDefault(a => a.ImdbId == imdbId);       
    }

    public IEnumerable<Actor> ReadAllActors()
    {
        return InMemoryStorage.Actors;
    }

    public IEnumerable<Actor> ReadActorsByNamePart(string namePart)
    {
        return InMemoryStorage.Actors
            .Where(a => a.Name.Contains(namePart, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Actor> ReadActorsByMinimumAge(int minimumAge)
    {
        return InMemoryStorage.Actors
            .Where(a => a.Age.HasValue && a.Age >= minimumAge);
    }

    public void UpdateActors(IEnumerable<Actor> actors)
    {
        foreach (var actor in actors)
        {
            var existing = InMemoryStorage.Actors.FirstOrDefault(a => a.ImdbId == actor.ImdbId);
            if (existing == null) continue;
            existing.Name = actor.Name;
            existing.Nationality = actor.Nationality;
            existing.DateOfBirth = actor.DateOfBirth;
            existing.Age = actor.Age;
        }
    }

    public void DeleteActor(Actor actor)
    {
        InMemoryStorage.Actors.Remove(actor);
    }
    
    //Director operations
    public void CreateDirector(FilmDirector director)
    {
        if (string.IsNullOrEmpty(director.ImdbId))
        {
            director.ImdbId = ImdbIdGenerator.GenerateDirectorId();
        }
        InMemoryStorage.FilmDirectors.Add(director);
    }

    public FilmDirector ReadDirector(string imdbId)
    {
        return InMemoryStorage.FilmDirectors.FirstOrDefault(d => d.ImdbId == imdbId);       
    }

    public FilmDirector ReadDirectorByName(string name)
    {
        return InMemoryStorage.FilmDirectors
            .FirstOrDefault(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<FilmDirector> ReadAllDirectors()
    {
        return InMemoryStorage.FilmDirectors;
    }

    public void UpdateDirectors(IEnumerable<FilmDirector> directors)
    {
        foreach (var director in directors)
        {
            var existing = InMemoryStorage.FilmDirectors.FirstOrDefault(d => d.ImdbId == director.ImdbId);
            if (existing == null) continue;
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