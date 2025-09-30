using ConsoleApp.Models;

namespace ConsoleApp.Services;

public class DataService
{
    public List<Actor> Actors { get; private set; } = [];
    public List<Film> Films { get; private set; } = [];

    public void Seed()
    {
        // Create directors (not part of many-to-many, just needed for films)
        var lanthimos = new FilmDirector { Name = "Yorgos Lanthimos", Country = "Greece", YearStarted = 2001 };
        var kubrick = new FilmDirector { Name = "Stanley Kubrick", Country = "USA", YearStarted = 1951 };
        var lynch = new FilmDirector { Name = "David Lynch", Country = "USA", YearStarted = 1970 };
        var eggers = new FilmDirector { Name = "Robert Eggers", Country = "USA", YearStarted = 2015 };

        // Create actors (part of many-to-many with films)
        var stone = new Actor
        {
            Name = "Emma Stone",
            Nationality = "American",
            DateOfBirth = new DateTime(1988, 11, 6),
            Age = 36
        };
        
        var farrell = new Actor
        {
            Name = "Colin Farrell",
            Nationality = "Irish",
            DateOfBirth = new DateTime(1976, 5, 31),
            Age = null
        };
        
        var kidman = new Actor
        {
            Name = "Nicole Kidman",
            Nationality = "Australian",
            DateOfBirth = new DateTime(1967, 6, 20),
            Age = 57
        };
        
        var pattinson = new Actor
        {
            Name = "Robert Pattinson",
            Nationality = "British",
            DateOfBirth = new DateTime(1986, 5, 13),
            Age = 38
        };
        
        var dafoe = new Actor
        {
            Name = "Willem Dafoe",
            Nationality = "American",
            DateOfBirth = new DateTime(1955, 7, 22),
            Age = null
        };
        
        var keir = new Actor
        {
            Name = "Barry Keoghan",
            Nationality = "Irish",
            DateOfBirth = new DateTime(1992, 10, 18),
            Age = 32
        };

        Actors.AddRange(new[] { stone, farrell, kidman, pattinson, dafoe, keir });

        // Create films (part of many-to-many with actors)
        var theLobster = new Film
        {
            Title = "The Lobster",
            Genre = Genre.Drama,
            ReleaseDate = new DateTime(2015, 10, 16),
            Rating = 7.2,
            Director = lanthimos
        };
        
        var poorThings = new Film
        {
            Title = "Poor Things",
            Genre = Genre.Comedy,
            ReleaseDate = new DateTime(2023, 12, 8),
            Rating = 7.9,
            Director = lanthimos
        };
        
        var theShining = new Film
        {
            Title = "The Shining",
            Genre = Genre.Horror,
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
            Genre = Genre.Horror,
            ReleaseDate = new DateTime(2019, 10, 18),
            Rating = 7.4,
            Director = eggers
        };
        
        var theNorthman = new Film
        {
            Title = "The Northman",
            Genre = Genre.Action,
            ReleaseDate = new DateTime(2022, 4, 22),
            Rating = 7.0,
            Director = eggers
        };

        Films.AddRange(new[] { theLobster, poorThings, theShining, mulhollandDrive, theLighthouse, theNorthman });

        // Establish many-to-many relationships
        // The Lobster - Farrell
        theLobster.AddActor(farrell);
        farrell.AddFilm(theLobster);
        
        // Poor Things - Stone, Dafoe
        poorThings.AddActor(stone);
        poorThings.AddActor(dafoe);
        stone.AddFilm(poorThings);
        dafoe.AddFilm(poorThings);
        
        // The Lighthouse - Pattinson, Dafoe
        theLighthouse.AddActor(pattinson);
        theLighthouse.AddActor(dafoe);
        pattinson.AddFilm(theLighthouse);
        dafoe.AddFilm(theLighthouse);
        
        // The Northman - Kidman, Dafoe
        theNorthman.AddActor(kidman);
        theNorthman.AddActor(dafoe);
        kidman.AddFilm(theNorthman);
        dafoe.AddFilm(theNorthman);
        
        // Add director relationships
        lanthimos.AddFilm(theLobster);
        lanthimos.AddFilm(poorThings);
        kubrick.AddFilm(theShining);
        lynch.AddFilm(mulhollandDrive);
        eggers.AddFilm(theLighthouse);
        eggers.AddFilm(theNorthman);
    }
}