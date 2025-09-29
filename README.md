# Film Database - Project .NET Framework

* Naam: Romeo Weyns
* Studentennummer: 0177605-95
* Academiejaar: 25-26
* Klasgroep: INF-202A
* Onderwerp: Film, Actor, FilmDirector

## UML Class Diagram

```mermaid
classDiagram
    class Actor {
        -List~Film~ _films
        +string Name
        +string Nationality
        +DateTime DateOfBirth
        +int? Age
        +IReadOnlyList~Film~ Films
        +AddFilm(Film film)
        +RemoveFilm(Film film)
        +ClearFilms()
        +AddFilmsRange(IEnumerable~Film~ films)
        +ToString() string
        -CalculateAge() int
    }

    class Film {
        -List~Actor~ _actors
        +string Title
        +Genre Genre
        +DateTime ReleaseDate
        +double Rating
        +FilmDirector Director
        +IReadOnlyList~Actor~ Actors
        +AddActor(Actor actor)
        +RemoveActor(Actor actor)
        +ClearActors()
        +AddActorsRange(IEnumerable~Actor~ actors)
        +ToString() string
    }

    class FilmDirector {
        -List~Film~ _films
        +string Name
        +string Country
        +DateTime CareerStart
        +IReadOnlyList~Film~ Films
        +AddFilm(Film film)
        +RemoveFilm(Film film)
        +ClearFilms()
        +AddFilmsRange(IEnumerable~Film~ films)
        +ToString() string
    }

    class Genre {
        <<enumeration>>
        Action
        Comedy
        Drama
        Horror
        Romance
        SciFi
        Thriller
        Documentary
    }

    class DataService {
        +List~Actor~ Actors
        +List~Film~ Films
        +List~FilmDirector~ Directors
        +Seed() void
    }

    class MenuService {
        -DataService _dataService
        +MenuService(DataService dataService)
        +ShowMainMenu() void
        +HandleMenuChoice(int choice) void
        -ShowAllFilms() void
        -ShowFilmsByGenre() void
        -ShowFilmsByDirector() void
        -ShowFilmsByGenreAndDirector() void
        -ShowFilmsByYearRange() void
        -ShowFilmsByRatingRange() void
        -ShowFilmsWithAdvancedFilters() void
        -ShowAllActors() void
        -ShowActorsWithCriteria() void
        -ShowActorsByNationality() void
        -ShowActorsByFilmCount() void
        -ShowAllDirectors() void
        -ShowDirectorsWithCriteria() void
    }

    class Program {
        +Main(string[] args)$ void
    }

    Actor "0..*" -- "0..*" Film : appears in
    Film "0..*" --> "1" FilmDirector : directed by
    FilmDirector "1" -- "0..*" Film : directs
    Film --> Genre : has

    DataService "1" --> "*" Actor : manages
    DataService "1" --> "*" Film : manages
    DataService "1" --> "*" FilmDirector : manages
    MenuService "1" --> "1" DataService : uses
    Program ..> DataService : creates
    Program ..> MenuService : creates
```