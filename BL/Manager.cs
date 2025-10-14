using System.ComponentModel.DataAnnotations;
using FilmManagement.BL.Domain;
using FilmManagement.DAL;

namespace FilmManagement.BL;

public class Manager(IRepository repository) : IManager
{
    private void ValidateEntity<T>(T entity)
    {
        List<ValidationResult> errors = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(entity, 
            new ValidationContext(entity),
            errors, validateAllProperties: true);

        if (isValid) return;
        var errorMessages = errors.Select(e => e.ErrorMessage);
        throw new ArgumentException(string.Join(" | ", errorMessages));
    }

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
        
        ValidateEntity(filmToCreate);
        repository.CreateFilm(filmToCreate);
        return filmToCreate;
    }

    public Film GetFilm(string imdbId)
    {
        return repository.ReadFilm(imdbId);       
    }

    public IEnumerable<Film> GetFilms()
    {
        return repository.ReadAllFilms();
    }

    public IEnumerable<Film> GetFilmsByGenre(Genre genre)
    {
        return repository.ReadFilmsByGenre(genre);       
    }

    public void ChangeFilms(IEnumerable<Film> films)
    {
        foreach (var film in films)
        {
            ValidateEntity(film);
        }
        repository.UpdateFilms(films);
    }
    
    public void RemoveFilm(Film film)
    {
        repository.DeleteFilm(film);
    }

    public Actor AddActor(string name, string nationality, DateTime dateOfBirth, int? age)
    {
        var actorToCreate = new Actor
        {
            Name = name,
            Nationality = nationality,
            DateOfBirth = dateOfBirth,
            Age = age
        };
        
        ValidateEntity(actorToCreate);
        repository.CreateActor(actorToCreate);
        return actorToCreate;
    }

    public Actor GetActor(string imdbId)
    {
        return repository.ReadActor(imdbId);       
    }

    public IEnumerable<Actor> GetActors()
    {
        return repository.ReadAllActors();
    }

    public IEnumerable<Actor> GetActorsByNationality(string nationality)
    {
        return repository.ReadActorsByNationality(nationality);       
    }

    public IEnumerable<Actor> GetActorsByAge(int age)
    {
        return repository.ReadActorsByAge(age);       
    }

    public void ChangeActors(IEnumerable<Actor> actors)
    {
        foreach (var actor in actors)
        {
            ValidateEntity(actor);
        }
        repository.UpdateActors(actors);
    }
    
    public void RemoveActor(Actor actor)
    {
        repository.DeleteActor(actor);
    }

    public FilmDirector AddDirector(string name, string country, int? yearStarted)
    {
        var filmDirectorToCreate = new FilmDirector
        {
            Name = name,
            Country = country,
            YearStarted = yearStarted       
        };
        
        ValidateEntity(filmDirectorToCreate);
        repository.CreateDirector(filmDirectorToCreate);
        return filmDirectorToCreate;
    }

    public FilmDirector GetDirector(string imdbId)
    {
        return repository.ReadDirector(imdbId);      
    }

    public IEnumerable<FilmDirector> GetDirectors()
    {
        return repository.ReadAllDirectors();
    }
    
    public void ChangeDirectors(IEnumerable<FilmDirector> directors)
    {
        foreach (var director in directors)
        {
            ValidateEntity(director);
        }
        repository.UpdateDirectors(directors);
    }
    
    public void RemoveDirector(FilmDirector director)
    {
        repository.DeleteDirector(director);
    }
}