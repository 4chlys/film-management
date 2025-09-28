using ConsoleApp.Models;

namespace ConsoleApp.Services;

public class DataService
    {
        public List<Actor> Actors { get; private set; } = new List<Actor>();
        public List<Film> Films { get; private set; } = new List<Film>();
        public List<FilmDirector> Directors { get; private set; } = new List<FilmDirector>();

        public void Seed()
        {
            // Create directors
            var lanthimos = new FilmDirector 
            { 
                Name = "Yorgos Lanthimos", 
                Country = "Greece", 
                CareerStart = new DateTime(2001, 3, 11) 
            };
            var kubrick = new FilmDirector 
            { 
                Name = "Stanley Kubrick", 
                Country = "USA", 
                CareerStart = new DateTime(1951, 12, 31) 
            };
            var lynch = new FilmDirector 
            { 
                Name = "David Lynch", 
                Country = "USA", 
                CareerStart = new DateTime(1970, 5, 19) 
            };
            var eggers = new FilmDirector 
            { 
                Name = "Robert Eggers", 
                Country = "USA", 
                CareerStart = new DateTime(2015, 11, 4) 
            };
            var tarkovsky = new FilmDirector 
            { 
                Name = "Andrei Tarkovsky", 
                Country = "Russia", 
                CareerStart = new DateTime(1962, 4, 23) 
            };
            var godard = new FilmDirector 
            { 
                Name = "Jean-Luc Godard", 
                Country = "France", 
                CareerStart = new DateTime(1954, 7, 12) 
            };
            var fassbinder = new FilmDirector 
            { 
                Name = "Rainer Werner Fassbinder", 
                Country = "Germany", 
                CareerStart = new DateTime(1965, 2, 6) 
            };

            Directors.AddRange(new[] { lanthimos, kubrick, lynch, eggers, tarkovsky, godard, fassbinder });

            // Create actors
            var stone = new Actor 
            { 
                Name = "Emma Stone", 
                Nationality = "American", 
                DateOfBirth = new DateTime(1988, 11, 6),
                Age = 35
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
                Age = 56
            };
            var pattinson = new Actor 
            { 
                Name = "Robert Pattinson", 
                Nationality = "British", 
                DateOfBirth = new DateTime(1986, 5, 13),
                Age = 37
            };
            var dafoe = new Actor 
            { 
                Name = "Willem Dafoe", 
                Nationality = "American", 
                DateOfBirth = new DateTime(1955, 7, 22),
                Age = null // nullable example
            };
            var keir = new Actor 
            { 
                Name = "Barry Keoghan", 
                Nationality = "Irish", 
                DateOfBirth = new DateTime(1992, 10, 18),
                Age = 31
            };
            var weisz = new Actor 
            { 
                Name = "Rachel Weisz", 
                Nationality = "British", 
                DateOfBirth = new DateTime(1970, 3, 7),
                Age = 53
            };
            var dern = new Actor 
            { 
                Name = "Laura Dern", 
                Nationality = "American", 
                DateOfBirth = new DateTime(1967, 2, 10),
                Age = 56
            };
            var maclachlan = new Actor 
            { 
                Name = "Kyle MacLachlan", 
                Nationality = "American", 
                DateOfBirth = new DateTime(1959, 2, 22),
                Age = 64
            };
            var rossellini = new Actor 
            { 
                Name = "Isabella Rossellini", 
                Nationality = "Italian", 
                DateOfBirth = new DateTime(1952, 6, 18),
                Age = 71
            };
            var hopper = new Actor 
            { 
                Name = "Dennis Hopper", 
                Nationality = "American", 
                DateOfBirth = new DateTime(1936, 5, 17),
                Age = null // nullable example
            };
            var belmondo = new Actor 
            { 
                Name = "Jean-Paul Belmondo", 
                Nationality = "French", 
                DateOfBirth = new DateTime(1933, 4, 9),
                Age = null // nullable example
            };

            Actors.AddRange(new[] { stone, farrell, kidman, pattinson, dafoe, keir, weisz, dern, maclachlan, rossellini, hopper, belmondo });

            // Create films and establish relationships
            var theLobster = new Film
            {
                Title = "The Lobster",
                Genre = Genre.Drama,
                ReleaseDate = new DateTime(2015, 10, 16),
                Rating = 7.2,
                Director = lanthimos
            };
            theLobster.Actors.AddRange(new[] { farrell, weisz });

            var theFavourite = new Film
            {
                Title = "The Favourite",
                Genre = Genre.Drama,
                ReleaseDate = new DateTime(2018, 11, 23),
                Rating = 7.5,
                Director = lanthimos
            };
            theFavourite.Actors.AddRange(new[] { stone, weisz });

            var poorThings = new Film
            {
                Title = "Poor Things",
                Genre = Genre.Comedy,
                ReleaseDate = new DateTime(2023, 12, 8),
                Rating = 7.9,
                Director = lanthimos
            };
            poorThings.Actors.AddRange(new[] { stone, dafoe });

            var aClockworkOrange = new Film
            {
                Title = "A Clockwork Orange",
                Genre = Genre.Drama,
                ReleaseDate = new DateTime(1971, 12, 19),
                Rating = 8.3,
                Director = kubrick
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
            mulhollandDrive.Actors.AddRange(new[] { dern });

            var blueVelvet = new Film
            {
                Title = "Blue Velvet",
                Genre = Genre.Thriller,
                ReleaseDate = new DateTime(1986, 9, 19),
                Rating = 7.7,
                Director = lynch
            };
            blueVelvet.Actors.AddRange(new[] { maclachlan, rossellini, hopper });

            var theWitch = new Film
            {
                Title = "The Witch",
                Genre = Genre.Horror,
                ReleaseDate = new DateTime(2015, 2, 19),
                Rating = 6.9,
                Director = eggers
            };

            var theLighthouse = new Film
            {
                Title = "The Lighthouse",
                Genre = Genre.Horror,
                ReleaseDate = new DateTime(2019, 10, 18),
                Rating = 7.4,
                Director = eggers
            };
            theLighthouse.Actors.AddRange(new[] { pattinson, dafoe });

            var theNorthman = new Film
            {
                Title = "The Northman",
                Genre = Genre.Action,
                ReleaseDate = new DateTime(2022, 4, 22),
                Rating = 7.0,
                Director = eggers
            };
            theNorthman.Actors.AddRange(new[] { kidman, dafoe });

            var stalker = new Film
            {
                Title = "Stalker",
                Genre = Genre.SciFi,
                ReleaseDate = new DateTime(1979, 5, 25),
                Rating = 8.0,
                Director = tarkovsky
            };

            var solaris = new Film
            {
                Title = "Solaris",
                Genre = Genre.SciFi,
                ReleaseDate = new DateTime(1972, 2, 5),
                Rating = 8.1,
                Director = tarkovsky
            };

            var breathless = new Film
            {
                Title = "Breathless",
                Genre = Genre.Drama,
                ReleaseDate = new DateTime(1960, 3, 16),
                Rating = 7.7,
                Director = godard
            };
            breathless.Actors.Add(belmondo);

            var aliInWonderland = new Film
            {
                Title = "Ali: Fear Eats the Soul",
                Genre = Genre.Drama,
                ReleaseDate = new DateTime(1974, 3, 5),
                Rating = 8.0,
                Director = fassbinder
            };

            var theSacredDeer = new Film
            {
                Title = "The Killing of a Sacred Deer",
                Genre = Genre.Thriller,
                ReleaseDate = new DateTime(2017, 10, 20),
                Rating = 7.0,
                Director = lanthimos
            };
            theSacredDeer.Actors.AddRange(new[] { farrell, kidman, keir });

            Films.AddRange(new[] { theLobster, theFavourite, poorThings, aClockworkOrange, theShining, 
                                   mulhollandDrive, blueVelvet, theWitch, theLighthouse, theNorthman,
                                   stalker, solaris, breathless, aliInWonderland, theSacredDeer });

            // Update actor-film relationships
            stone.Films.AddRange(new[] { theFavourite, poorThings });
            farrell.Films.AddRange(new[] { theLobster, theSacredDeer });
            kidman.Films.AddRange(new[] { theNorthman, theSacredDeer });
            pattinson.Films.Add(theLighthouse);
            dafoe.Films.AddRange(new[] { poorThings, theLighthouse, theNorthman });
            keir.Films.Add(theSacredDeer);
            weisz.Films.AddRange(new[] { theLobster, theFavourite });
            dern.Films.Add(mulhollandDrive);
            maclachlan.Films.Add(blueVelvet);
            rossellini.Films.Add(blueVelvet);
            hopper.Films.Add(blueVelvet);
            belmondo.Films.Add(breathless);

            // Update director-film relationships
            lanthimos.Films.AddRange(new[] { theLobster, theFavourite, poorThings, theSacredDeer });
            kubrick.Films.AddRange(new[] { aClockworkOrange, theShining });
            lynch.Films.AddRange(new[] { mulhollandDrive, blueVelvet });
            eggers.Films.AddRange(new[] { theWitch, theLighthouse, theNorthman });
            tarkovsky.Films.AddRange(new[] { stalker, solaris });
            godard.Films.Add(breathless);
            fassbinder.Films.Add(aliInWonderland);
        }
    }