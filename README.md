# Film Database - Project .NET Framework

* Naam: Romeo Weyns
* Studentennummer: 0177605-95
* Academiejaar: 25-26
* Klasgroep: INF-202A
* Onderwerp: Film, Actor, FilmDirector

## Sprint 1 

### UML Class Diagram

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
        +List~FilmDirectors~ Directors
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

## Sprint 3

### Beide zoekcriteria ingevuld

```sql
SELECT "a"."ImdbId", "a"."Age", "a"."DateOfBirth", "a"."Name", "a"."Nationality"
FROM "Actors" AS "a"
WHERE instr(upper("a"."Name"), @__ToUpper_0) > 0 AND "a"."Age" >= @__minimumAge_1
-- Name = "Emma" AND MinimumAge = 20
```

### Enkel zoeken op naam

```sql
SELECT "a"."ImdbId", "a"."Age", "a"."DateOfBirth", "a"."Name", "a"."Nationality"
FROM "Actors" AS "a"
WHERE instr(upper("a"."Name"), @__ToUpper_0) > 0
-- Name = "Emma"
```

### Enkel zoeken op minimum leeftijd

```sql
SELECT "a"."ImdbId", "a"."Age", "a"."DateOfBirth", "a"."Name", "a"."Nationality"
FROM "Actors" AS "a"
WHERE "a"."Age" >= @__minimumAge_0
-- MinimumAge = 20
```

### Beide zoekcriteria leeg

```sql
SELECT "a"."ImdbId", "a"."Age", "a"."DateOfBirth", "a"."Name", "a"."Nationality"
FROM "Actors" AS "a"
```

---