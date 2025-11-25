using FilmManagement.BL.Domain;

namespace FilmManagement.DAL.EF;

public static class DataSeeder
{
    public static void Seed(FilmDbContext context)
    {
        // === DIRECTORS ===
        var lanthimos = new Director 
        { 
            Name = "Yorgos Lanthimos", 
            Country = "Greece", 
            CareerStart = 2001
        };
        
        var kubrick = new Director 
        { 
            Name = "Stanley Kubrick", 
            Country = "USA", 
            CareerStart = 1951,
            CareerEnd = 1999
        };
        
        var lynch = new Director 
        { 
            Name = "David Lynch", 
            Country = "USA", 
            CareerStart = 1970,
            CareerEnd = 2006
        };
        
        var eggers = new Director 
        { 
            Name = "Robert Eggers", 
            Country = "USA", 
            CareerStart = 2015
        };

        var godard = new Director
        {
            Name = "Jean-Luc Godard",
            Country = "France",
            CareerStart = 1950,
            CareerEnd = 2018
        };

        context.Directors.AddRange(lanthimos, kubrick, lynch, eggers, godard);
        

        // === ACTORS ===
        var stone = new Actor
        {
            Name = "Emma Stone",
            Nationality = "American",
            DateOfBirth = new DateTime(1988, 11, 6)
        };
        
        var farrell = new Actor
        {
            Name = "Colin Farrell",
            Nationality = "Irish",
            DateOfBirth = new DateTime(1976, 5, 31),
        };
        
        var kidman = new Actor
        {
            Name = "Nicole Kidman",
            Nationality = "Australian",
            DateOfBirth = new DateTime(1967, 6, 20),
        };
        
        var pattinson = new Actor
        {
            Name = "Robert Pattinson",
            Nationality = "British",
            DateOfBirth = new DateTime(1986, 5, 13),
        };
        
        var dafoe = new Actor
        {
            Name = "Willem Dafoe",
            Nationality = "American",
            DateOfBirth = new DateTime(1955, 7, 22),
        };
        
        var keir = new Actor
        {
            Name = "Barry Keoghan",
            Nationality = "Irish",
            DateOfBirth = new DateTime(1992, 10, 18),
        };
        
        var belmondo = new Actor
        {
            Name = "Jean-Paul Belmondo",
            Nationality = "French",
            DateOfBirth = new DateTime(1933, 4, 9)
        };

        var karina = new Actor
        {
            Name = "Anna Karina",
            Nationality = "Danish-French",
            DateOfBirth = new DateTime(1940, 9, 22)
        };

        context.Actors.AddRange(stone, farrell, kidman, pattinson, dafoe, keir, belmondo, karina);
        

        // === FILMS ===
        var theLobster = new Film
        {
            Title = "The Lobster",
            Genre = Genre.Drama | Genre.Romance,
            ReleaseDate = new DateTime(2015, 10, 16),
            Rating = 7.2,
            Director = lanthimos
        };
        
        var poorThings = new Film
        {
            Title = "Poor Things",
            Genre = Genre.Comedy | Genre.Romance,
            ReleaseDate = new DateTime(2023, 12, 8),
            Rating = 7.9,
            Director = lanthimos
        };
        
        var theShining = new Film
        {
            Title = "The Shining",
            Genre = Genre.Horror | Genre.Thriller,
            ReleaseDate = new DateTime(1980, 5, 23),
            Rating = 8.4,
            Director = kubrick
        };
        
        var mulhollandDrive = new Film
        {
            Title = "Mulholland Drive",
            Genre = Genre.Thriller,
            ReleaseDate = new DateTime(2001, 10, 12),
            Rating = 7.9,
            Director = lynch
        };
        
        var theLighthouse = new Film
        {
            Title = "The Lighthouse",
            Genre = Genre.Horror | Genre.Thriller,
            ReleaseDate = new DateTime(2019, 10, 18),
            Rating = 7.4,
            Director = eggers
        };
        
        var theNorthman = new Film
        {
            Title = "The Northman",
            Genre = Genre.Action | Genre.Romance | Genre.Drama,
            ReleaseDate = new DateTime(2022, 4, 22),
            Rating = 7.0,
            Director = eggers
        };
        
        var breathless = new Film
        {
            Title = "À bout de souffle (Breathless)",
            Genre = Genre.Drama | Genre.Romance,
            ReleaseDate = new DateTime(1960, 3, 16),
            Rating = 7.8,
            Director = godard
        };

        var vivreSaVie = new Film
        {
            Title = "Vivre sa vie",
            Genre = Genre.Drama,
            ReleaseDate = new DateTime(1962, 9, 20),
            Rating = 8.0,
            Director = godard
        };

        context.Films.AddRange(
            theLobster, poorThings, theShining, mulhollandDrive,
            theLighthouse, theNorthman, breathless, vivreSaVie
        );


        // === RELATIONSHIPS ===
        // Many to many relationships
        theLobster.ActorFilms.Add(new ActorFilm { Actor = farrell, Film = theLobster });

        poorThings.ActorFilms.Add(new ActorFilm { Actor = stone, Film = poorThings });
        poorThings.ActorFilms.Add(new ActorFilm { Actor = dafoe, Film = poorThings });

        theLighthouse.ActorFilms.Add(new ActorFilm { Actor = pattinson, Film = theLighthouse });
        theLighthouse.ActorFilms.Add(new ActorFilm { Actor = dafoe, Film = theLighthouse });

        theNorthman.ActorFilms.Add(new ActorFilm { Actor = kidman, Film = theNorthman });
        theNorthman.ActorFilms.Add(new ActorFilm { Actor = dafoe, Film = theNorthman });

        breathless.ActorFilms.Add(new ActorFilm { Actor = belmondo, Film = breathless });

        vivreSaVie.ActorFilms.Add(new ActorFilm { Actor = karina, Film = vivreSaVie });
        
        context.SaveChanges();
        context.ChangeTracker.Clear();
    }
}
