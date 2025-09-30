using ConsoleApp.Models;

namespace ConsoleApp.Services;

public class DataService
{
    public List<Actor> Actors { get; private set; } = new();
    public List<Film> Films { get; private set; } = new();
    public List<FilmDirector> Directors { get; private set; } = new();

    public void Seed()
    {
        // Create directors
        var lanthimos = new FilmDirector("Yorgos Lanthimos", new DateTime(2001, 3, 11))
        {
            Country = "Greece"
        };
        var kubrick = new FilmDirector("Stanley Kubrick", new DateTime(1951, 12, 31))
        {
            Country = "USA"
        };
        var lynch = new FilmDirector("David Lynch", new DateTime(1970, 5, 19))
        {
            Country = "USA"
        };
        var eggers = new FilmDirector("Robert Eggers", new DateTime(2015, 11, 4))
        {
            Country = "USA"
        };
        var tarkovsky = new FilmDirector("Andrei Tarkovsky", new DateTime(1962, 4, 23))
        {
            Country = "Russia"
        };
        var godard = new FilmDirector("Jean-Luc Godard", new DateTime(1954, 7, 12))
        {
            Country = "France"
        };
        var fassbinder = new FilmDirector("Rainer Werner Fassbinder", new DateTime(1965, 2, 6))
        {
            Country = "Germany"
        };

        Directors.AddRange([lanthimos, kubrick, lynch, eggers, tarkovsky, godard, fassbinder]);

        // Create actors
        var stone = new Actor("Emma Stone", new DateTime(1988, 11, 6))
        {
            Nationality = "American",
            Age = 35
        };
        var farrell = new Actor("Colin Farrell", new DateTime(1976, 5, 31))
        {
            Nationality = "Irish"
        };
        var kidman = new Actor("Nicole Kidman", new DateTime(1967, 6, 20))
        {
            Nationality = "Australian",
            Age = 56
        };
        var pattinson = new Actor("Robert Pattinson", new DateTime(1986, 5, 13))
        {
            Nationality = "British",
            Age = 37
        };
        var dafoe = new Actor("Willem Dafoe", new DateTime(1955, 7, 22))
        {
            Nationality = "American"
        };
        var keir = new Actor("Barry Keoghan", new DateTime(1992, 10, 18))
        {
            Nationality = "Irish",
            Age = 31
        };
        var weisz = new Actor("Rachel Weisz", new DateTime(1970, 3, 7))
        {
            Nationality = "British",
            Age = 53
        };
        var dern = new Actor("Laura Dern", new DateTime(1967, 2, 10))
        {
            Nationality = "American",
            Age = 56
        };
        var maclachlan = new Actor("Kyle MacLachlan", new DateTime(1959, 2, 22))
        {
            Nationality = "American",
            Age = 64
        };
        var rossellini = new Actor("Isabella Rossellini", new DateTime(1952, 6, 18))
        {
            Nationality = "Italian",
            Age = 71
        };
        var hopper = new Actor("Dennis Hopper", new DateTime(1936, 5, 17))
        {
            Nationality = "American"
        };
        var belmondo = new Actor("Jean-Paul Belmondo", new DateTime(1933, 4, 9))
        {
            Nationality = "French"
        };

        Actors.AddRange([stone, farrell, kidman, pattinson, dafoe, keir, weisz, dern, maclachlan, rossellini, hopper, belmondo]);

        // Create films and establish relationships
        var theLobster = new Film("The Lobster", lanthimos)
        {
            Genre = Genre.Drama,
            ReleaseDate = new DateTime(2015, 10, 16),
            Rating = 7.2
        };
        theLobster.AddActorsRange([farrell, weisz]);

        var theFavourite = new Film("The Favourite", lanthimos)
        {
            Genre = Genre.Drama,
            ReleaseDate = new DateTime(2018, 11, 23),
            Rating = 7.5
        };
        theFavourite.AddActorsRange([stone, weisz]);

        var poorThings = new Film("Poor Things", lanthimos)
        {
            Genre = Genre.Comedy,
            ReleaseDate = new DateTime(2023, 12, 8),
            Rating = 7.9
        };
        poorThings.AddActorsRange([stone, dafoe]);

        var aClockworkOrange = new Film("A Clockwork Orange", kubrick)
        {
            Genre = Genre.Drama,
            ReleaseDate = new DateTime(1971, 12, 19),
            Rating = 8.3
        };

        var theShining = new Film("The Shining", kubrick)
        {
            Genre = Genre.Horror,
            ReleaseDate = new DateTime(1980, 5, 23),
            Rating = 8.4
        };

        var mulhollandDrive = new Film("Mulholland Drive", lynch)
        {
            Genre = Genre.Thriller,
            ReleaseDate = new DateTime(2001, 10, 12),
            Rating = 7.9
        };
        mulhollandDrive.AddActor(dern);

        var blueVelvet = new Film("Blue Velvet", lynch)
        {
            Genre = Genre.Thriller,
            ReleaseDate = new DateTime(1986, 9, 19),
            Rating = 7.7
        };
        blueVelvet.AddActorsRange([maclachlan, rossellini, hopper]);

        var theWitch = new Film("The Witch", eggers)
        {
            Genre = Genre.Horror,
            ReleaseDate = new DateTime(2015, 2, 19),
            Rating = 6.9
        };

        var theLighthouse = new Film("The Lighthouse", eggers)
        {
            Genre = Genre.Horror,
            ReleaseDate = new DateTime(2019, 10, 18),
            Rating = 7.4
        };
        theLighthouse.AddActorsRange([pattinson, dafoe]);

        var theNorthman = new Film("The Northman", eggers)
        {
            Genre = Genre.Action,
            ReleaseDate = new DateTime(2022, 4, 22),
            Rating = 7.0
        };
        theNorthman.AddActorsRange([kidman, dafoe]);

        var stalker = new Film("Stalker", tarkovsky)
        {
            Genre = Genre.SciFi,
            ReleaseDate = new DateTime(1979, 5, 25),
            Rating = 8.0
        };

        var solaris = new Film("Solaris", tarkovsky)
        {
            Genre = Genre.SciFi,
            ReleaseDate = new DateTime(1972, 2, 5),
            Rating = 8.1
        };

        var breathless = new Film("Breathless", godard)
        {
            Genre = Genre.Drama,
            ReleaseDate = new DateTime(1960, 3, 16),
            Rating = 7.7
        };
        breathless.AddActor(belmondo);

        var aliInWonderland = new Film("Ali: Fear Eats the Soul", fassbinder)
        {
            Genre = Genre.Drama,
            ReleaseDate = new DateTime(1974, 3, 5),
            Rating = 8.0
        };

        var theSacredDeer = new Film("The Killing of a Sacred Deer", lanthimos)
        {
            Genre = Genre.Thriller,
            ReleaseDate = new DateTime(2017, 10, 20),
            Rating = 7.0
        };
        theSacredDeer.AddActorsRange([farrell, kidman, keir]);

        Films.AddRange([theLobster, theFavourite, poorThings, aClockworkOrange, theShining, 
                               mulhollandDrive, blueVelvet, theWitch, theLighthouse, theNorthman,
                               stalker, solaris, breathless, aliInWonderland, theSacredDeer]);

        // Update actor-film relationships
        stone.AddFilmsRange([theFavourite, poorThings]);
        farrell.AddFilmsRange([theLobster, theSacredDeer]);
        kidman.AddFilmsRange([theNorthman, theSacredDeer]);
        pattinson.AddFilm(theLighthouse);
        dafoe.AddFilmsRange([poorThings, theLighthouse, theNorthman]);
        keir.AddFilm(theSacredDeer);
        weisz.AddFilmsRange([theLobster, theFavourite]);
        dern.AddFilm(mulhollandDrive);
        maclachlan.AddFilm(blueVelvet);
        rossellini.AddFilm(blueVelvet);
        hopper.AddFilm(blueVelvet);
        belmondo.AddFilm(breathless);

        // Update director-film relationships
        lanthimos.AddFilmsRange([theLobster, theFavourite, poorThings, theSacredDeer]);
        kubrick.AddFilmsRange([aClockworkOrange, theShining]);
        lynch.AddFilmsRange([mulhollandDrive, blueVelvet]);
        eggers.AddFilmsRange([theWitch, theLighthouse, theNorthman]);
        tarkovsky.AddFilmsRange([stalker, solaris]);
        godard.AddFilm(breathless);
        fassbinder.AddFilm(aliInWonderland);
    }
}