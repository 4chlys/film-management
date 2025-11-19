using System.Linq.Expressions;
using FilmManagement.BL.Domain;
using FilmManagement.BL.Domain.Validation;
using FilmManagement.DAL;

namespace FilmManagement.BL;

public class Manager(IRepository repository) : IManager
{
    public Film AddFilm(string title, Genre genre, DateTime releaseDate, double rating, FilmDirector director)
    {
        var filmToCreate = new Film
        {
            Title = title,
            Genre = genre,
            ReleaseDate = releaseDate,
            Rating = rating,
            Director = director
        };
        
        EntityValidator.ValidateEntity(filmToCreate);
        repository.CreateFilm(filmToCreate);
        return filmToCreate;
    }

    public void AddActorToFilm(Guid filmId, Guid actorId, int? screenTime)
    {
        var actorFilm = new ActorFilm()
        {
            Actor = repository.ReadActor(actorId),
            Film = repository.ReadFilm(filmId),
            ScreenTime = screenTime
        };
        repository.CreateActorFilm(actorFilm);
    }

    public Film GetFilm(Guid imdbId)
    {
        return repository.ReadFilm(imdbId);       
    }

    public IEnumerable<Film> GetAllFilms()
    {
        return repository.ReadAllFilmsWithActorsAndDirectors();
    }

    public IEnumerable<Film> GetAllFilmsWithActorsAndDirectors()
    {
        return repository.ReadAllFilmsWithActorsAndDirectors();
    }

    public IEnumerable<Film> GetFilmsByGenre(Genre genre)
    {
        Expression<Func<Film, bool>> predicate = f => (f.Genre & genre) != 0;
        
        return repository.ReadFilmsByCriteria(predicate);
    }

    public void ChangeFilms(IEnumerable<Film> films)
    {
        foreach (var film in films)
        {
            EntityValidator.ValidateEntity(film);
        }
        repository.UpdateFilms(films);
    }
    
    public void RemoveFilm(Film film)
    {
        repository.DeleteFilm(film);
    }

    public void RemoveActorOfFilm(Guid filmId, Guid actorId)
    {
        var actorFilm = new ActorFilm()
        {
            Actor = repository.ReadActor(actorId),
            Film = repository.ReadFilm(filmId)
        };
        repository.DeleteActorFilm(actorFilm);
    }

    public Actor AddActor(string name, string nationality, DateTime dateOfBirth, DateTime? dateOfDeath)
    {
        var actorToCreate = new Actor
        {
            Name = name,
            Nationality = nationality,
            DateOfBirth = dateOfBirth,
            DateOfDeath = dateOfDeath      
        };
    
        EntityValidator.ValidateEntity(actorToCreate);
        repository.CreateActor(actorToCreate);
        return actorToCreate;
    }

    public Actor GetActor(Guid imdbId)
    {
        return repository.ReadActor(imdbId);       
    }

    public IEnumerable<Actor> GetAllActors()
    {
        return repository.ReadAllActorsWithFilms();
    }

    public IEnumerable<Actor> GetAllActorsWithFilms()
    {
        return repository.ReadAllActorsWithFilms();
    }

    public IEnumerable<Actor> GetActorsByCriteria(string nameFilter, int? minimumAge)
    {
        Expression<Func<Actor, bool>> predicate = actor => true;
        
        if (!string.IsNullOrWhiteSpace(nameFilter))
        {
            var upperFilter = nameFilter.ToUpper();
            predicate = CombinePredicates(predicate, 
                a => a.Name.ToUpper().Contains(upperFilter));
        }
        
        if (minimumAge.HasValue)
        {
            var maxDateOfBirth = DateTime.Today.AddYears(-minimumAge.Value);
            predicate = CombinePredicates(predicate, 
                a => a.DateOfBirth <= maxDateOfBirth);
        }
        
        return repository.ReadActorsByCriteria(predicate);
    }

    public void ChangeActors(IEnumerable<Actor> actors)
    {
        foreach (var actor in actors)
        {
            EntityValidator.ValidateEntity(actor);
        }
        repository.UpdateActors(actors);
    }
    
    public void RemoveActor(Actor actor)
    {
        repository.DeleteActor(actor);
    }

    public FilmDirector AddDirector(string name, string country, int? yearStarted, int? yearEnded)
    {
        var filmDirectorToCreate = new FilmDirector
        {
            Name = name,
            Country = country,
            CareerStart = yearStarted,
            CareerEnd = yearEnded
        };
        
        EntityValidator.ValidateEntity(filmDirectorToCreate);
        repository.CreateDirector(filmDirectorToCreate);
        return filmDirectorToCreate;
    }

    public FilmDirector GetDirector(Guid imdbId)
    {
        return repository.ReadDirector(imdbId);      
    }

    public FilmDirector GetDirectorByName(string name)
    {
        return repository.ReadDirectorByName(name);
    }

    public IEnumerable<FilmDirector> GetAllDirectors()
    {
        return repository.ReadAllDirectorsWithFilms();
    }

    public IEnumerable<FilmDirector> GetAllDirectorsWithFilms()
    {
        return repository.ReadAllDirectorsWithFilms();
    }
    
    public void ChangeDirectors(IEnumerable<FilmDirector> directors)
    {
        foreach (var director in directors)
        {
            EntityValidator.ValidateEntity(director);
        }
        repository.UpdateDirectors(directors);
    }
    
    public void RemoveDirector(FilmDirector director)
    {
        repository.DeleteDirector(director);
    }

    public IEnumerable<Genre> GetAllGenres()
    {
        return Enum.GetValues<Genre>();
    }
    
    private Expression<Func<T, bool>> CombinePredicates<T>(
        Expression<Func<T, bool>> first, 
        Expression<Func<T, bool>> second)
    {
        var parameter = Expression.Parameter(typeof(T));
        
        var combined = Expression.AndAlso(
            Expression.Invoke(first, parameter),
            Expression.Invoke(second, parameter)
        );
        
        return Expression.Lambda<Func<T, bool>>(combined, parameter);
    }
}