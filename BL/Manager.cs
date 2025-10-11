using System.ComponentModel.DataAnnotations;
using FilmManagement.BL.Domain;
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
        
        List<ValidationResult> errors = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(filmToCreate, 
            new ValidationContext(filmToCreate),
            errors, validateAllProperties: true);

        if (!isValid)
        {
            throw new ArgumentException(string.Join("|", errors));
        }
        
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
        
        List<ValidationResult> errors = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(actorToCreate, 
            new ValidationContext(actorToCreate),
            errors, validateAllProperties: true);

        if (!isValid)
        {
            throw new ArgumentException(string.Join("|", errors));
        }
        
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
        
        List<ValidationResult> errors = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(filmDirectorToCreate, 
            new ValidationContext(filmDirectorToCreate),
            errors, validateAllProperties: true);

        if (!isValid)
        {
            throw new ArgumentException(string.Join("|", errors));
        }
        
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
        repository.UpdateDirectors(directors);
    }
    
    public void RemoveDirector(FilmDirector director)
    {
        repository.DeleteDirector(director);
    }
}