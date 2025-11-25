using System.ComponentModel.DataAnnotations;
using FilmManagement.BL;
using FilmManagement.UI.CA.Extensions;
using FilmManagement.UI.CA.Utilities;

namespace FilmManagement.UI.CA.Handlers;

public class ActorMenuHandler(IManager manager)
{
    public void ShowAllActors()
    {
        Console.WriteLine("\nAll actors");
        Console.WriteLine("==========");
        
        var actors = manager.GetAllActorsWithFilms();
        
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

        var filteredActors = manager.GetActorsByCriteria(nameFilter, ageFilter);

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

                manager.AddActor(name, nationality, dateOfBirth, dateOfDeath);
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
            var films = manager.GetAllFilmsWithActorsAndDirectors().ToList();
            
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

            var filmToAdd = manager.GetFilm(filmId);
            if (filmToAdd == null)
            {
                ValidationHelper.ShowErrorMessage("Film not found.");
                continue;
            }

            Console.WriteLine($"\nWhich actor would you like to add to '{filmToAdd.Title}'?");
            Console.WriteLine("==============================================");
            
            var actors = manager.GetAllActorsWithFilms().ToList();
            
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
                manager.AddActorToFilm(filmId, actorId, screenTime);
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
            var films = manager.GetAllFilmsWithActorsAndDirectors().ToList();
            
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

            var filmToRemoveFrom = manager.GetFilm(filmId);
            if (filmToRemoveFrom == null)
            {
                ValidationHelper.ShowErrorMessage("Film not found.");
                continue;
            }

            Console.WriteLine($"\nWhich actor would you like to remove from '{filmToRemoveFrom.Title}'?");
            Console.WriteLine("====================================================================");
            
            var actorsInFilm = manager.GetActorsOfFilm(filmId);
            
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
                manager.RemoveActorFromFilm(filmId, actorId);
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