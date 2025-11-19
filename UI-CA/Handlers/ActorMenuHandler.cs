using System.ComponentModel.DataAnnotations;
using FilmManagement.BL;
using FilmManagement.UI.CA.Extensions;
using FilmManagement.UI.CA.Utilities;

namespace FilmManagement.UI.CA.Handlers;

public class ActorMenuHandler
{
    private readonly IManager _manager;

    public ActorMenuHandler(IManager manager)
    {
        _manager = manager;
    }

    public void ShowAllActors()
    {
        Console.WriteLine("\nAll actors");
        Console.WriteLine("==========");
        
        var actors = _manager.GetAllActorsWithFilms();
        
        if (!actors.Any())
        {
            Console.WriteLine("No actors found.");
            return;
        }
        
        foreach (var actor in actors)
        {
            Console.WriteLine(actor.GetInfo());
        }
    }

    public void ShowActorsWithCriteria()
    {
        string nameFilter = InputParser.PromptForInput("Enter (part of) a name or leave blank: ");
        string ageInput = InputParser.PromptForInput("Enter minimum age or leave blank: ");
        
        int? ageFilter = InputParser.ParseOptionalInt(ageInput, "minimum age");

        var filteredActors = _manager.GetActorsByCriteria(nameFilter, ageFilter);

        Console.WriteLine($"\nFiltered actors:");
        Console.WriteLine("================");
        
        if (filteredActors.Any())
        {
            foreach (var actor in filteredActors)
            {
                Console.WriteLine(actor.GetInfo());
            }
        }
        else
        {
            Console.WriteLine("No actors found matching criteria.");
        }
    }

    public void AddActor()
    {
        Console.WriteLine("\nAdd actor");
        Console.WriteLine("=========");

        bool success = false;
        while (!success)
        {
            try
            {
                string name = InputParser.PromptForInput("Name: ");
                string nationality = InputParser.PromptForInput("Nationality: ");
                
                string dateOfBirthInput = InputParser.PromptForInput("Date of birth (yyyy-MM-dd): ");
                if (!InputParser.TryParseDateTime(dateOfBirthInput, out var dateOfBirth, "date of birth"))
                {
                    Console.WriteLine("Please try again...\n");
                    continue;
                }

                string dateOfDeathInput = InputParser.PromptForInput("Date of death (yyyy-MM-dd) (optional): ");
                DateTime? dateOfDeath = InputParser.ParseOptionalDateTime(dateOfDeathInput, "date of death");

                if (dateOfDeath.HasValue && dateOfDeathInput != "" && dateOfDeath.Value == default(DateTime))
                {
                    Console.WriteLine("Please try again...\n");
                    continue;
                }

                _manager.AddActor(name, nationality, dateOfBirth, dateOfDeath);
                ValidationHelper.ShowSuccessMessage("Actor added successfully!");
                success = true;
            }
            catch (ValidationException ex)
            {
                ValidationHelper.ShowValidationErrors(ex.Message);
                Console.WriteLine("Please try again...\n");
            }
        }
    }

    public void AddActorToFilm()
    {
        Console.WriteLine("\nWhich film would you like to add an actor to?");
        Console.WriteLine("==============================================");

        bool success = false;
        while (!success)
        {
            var films = _manager.GetAllFilmsWithActorsAndDirectors().ToList();
            
            if (!films.Any())
            {
                ValidationHelper.ShowErrorMessage("No films available.");
                return;
            }

            foreach (var film in films)
            {
                Console.WriteLine($"{film.ImdbId} - {film.Title}");
            }

            string filmIdInput = InputParser.PromptForInput("\nFilm ID: ");
            if (!InputParser.TryParseGuid(filmIdInput, out var filmId, "Film ID"))
            {
                continue;
            }

            var filmToAdd = _manager.GetFilm(filmId);
            if (filmToAdd == null)
            {
                ValidationHelper.ShowErrorMessage("Film not found.");
                continue;
            }

            Console.WriteLine($"\nWhich actor would you like to add to '{filmToAdd.Title}'?");
            Console.WriteLine("==============================================");
            
            var actors = _manager.GetAllActorsWithFilms().ToList();
            
            if (!actors.Any())
            {
                ValidationHelper.ShowErrorMessage("No actors available.");
                return;
            }

            foreach (var actor in actors)
            {
                Console.WriteLine($"{actor.ImdbId} - {actor.Name}");
            }

            string actorIdInput = InputParser.PromptForInput("\nActor ID: ");
            if (!InputParser.TryParseGuid(actorIdInput, out var actorId, "Actor ID"))
            {
                continue;
            }

            string screenTimeInput = InputParser.PromptForInput("Screen time in minutes (optional): ");
            int? screenTime = InputParser.ParseOptionalInt(screenTimeInput, "screen time");

            if (screenTimeInput != "" && !screenTime.HasValue)
            {
                continue;
            }

            try
            {
                _manager.AddActorToFilm(filmId, actorId, screenTime);
                ValidationHelper.ShowSuccessMessage("Actor added to film successfully!");
                success = true;
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowErrorMessage(ex.Message);
            }
        }
    }

    public void RemoveActorFromFilm()
    {
        Console.WriteLine("\nWhich film would you like to remove an actor from?");
        Console.WriteLine("===================================================");

        bool success = false;
        while (!success)
        {
            var films = _manager.GetAllFilmsWithActorsAndDirectors().ToList();
            
            if (!films.Any())
            {
                ValidationHelper.ShowErrorMessage("No films available.");
                return;
            }

            foreach (var film in films)
            {
                Console.WriteLine($"{film.ImdbId} - {film.Title}");
            }

            string filmIdInput = InputParser.PromptForInput("\nFilm ID: ");
            if (!InputParser.TryParseGuid(filmIdInput, out var filmId, "Film ID"))
            {
                continue;
            }

            var filmToRemove = _manager.GetFilm(filmId);
            if (filmToRemove == null)
            {
                ValidationHelper.ShowErrorMessage("Film not found.");
                continue;
            }

            Console.WriteLine($"\nWhich actor would you like to remove from '{filmToRemove.Title}'?");
            Console.WriteLine("====================================================================");
            
            var actorsInFilm = filmToRemove.ActorFilms.Select(af => af.Actor).ToList();
            
            if (!actorsInFilm.Any())
            {
                ValidationHelper.ShowErrorMessage("No actors in this film.");
                return;
            }

            foreach (var actor in actorsInFilm)
            {
                Console.WriteLine($"{actor.ImdbId} - {actor.Name}");
            }

            string actorIdInput = InputParser.PromptForInput("\nActor ID: ");
            if (!InputParser.TryParseGuid(actorIdInput, out var actorId, "Actor ID"))
            {
                continue;
            }

            try
            {
                _manager.RemoveActorOfFilm(filmId, actorId);
                ValidationHelper.ShowSuccessMessage("Actor removed from film successfully!");
                success = true;
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowErrorMessage(ex.Message);
            }
        }
    }
}