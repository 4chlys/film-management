using System.ComponentModel.DataAnnotations;
using FilmManagement.BL;
using FilmManagement.BL.Domain;
using FilmManagement.UI.CA.Extensions;

namespace FilmManagement.UI.CA;

public class ConsoleUi(IManager manager)
{
    private IManager _manager = manager;

    public void Run()
    {
        bool running = true;
        
        while (running)
        {
            ShowMainMenu();
        
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice == 0)
                {
                    running = false;
                    Console.WriteLine("Goodbye!");
                }
                else
                {
                    HandleMenuChoice(choice);
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid number.");
            }

        }
    }

    private void DisplayValidationErrors(string errorMessage)
    {
        var errors = errorMessage.Split('|', StringSplitOptions.RemoveEmptyEntries);
        Console.WriteLine("Validation errors:");
        foreach (var error in errors)
        {
            Console.WriteLine($"  - {error.Trim()}");
        }
    }

    public void ShowMainMenu()
    {
        Console.WriteLine("\nWhat would you like to do?");
        Console.WriteLine("==========================");
        Console.WriteLine("0) Quit");
        Console.WriteLine("1) Show all films");
        Console.WriteLine("2) Show films by genre");
        Console.WriteLine("3) Show all actors");
        Console.WriteLine("4) Show actors with name and/or minimum age");
        Console.WriteLine("5) Add a film");
        Console.WriteLine("6) Add an actor");
        Console.Write("Choice (0-6): ");
    }

    public void HandleMenuChoice(int choice)
    {
        switch (choice)
        {
            case 1:
                ShowAllFilms();
                break;
            case 2:
                ShowFilmsByGenre();
                break;
            case 3:
                ShowAllActors();
                break;
            case 4:
                ShowActorsWithCriteria();
                break;
            case 5:
                AddFilm();
                break;
            case 6:
                AddActor();
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }

    private void ShowAllFilms()
    {
        Console.WriteLine("\nAll films");
        Console.WriteLine("=========");
        foreach (var film in _manager.GetFilms())
        {
            Console.WriteLine(film.GetInfo());
        }
    }

    private void ShowFilmsByGenre()
    {
        Console.WriteLine("\nSelect genre:");
        Console.WriteLine("==============");
        
        foreach (var name in EnumExtensions.GetAllDisplayNames<Genre>())
        {
            Console.WriteLine($"- {name}");
        }

        Genre? selectedGenre = null;
        while (selectedGenre is null)
        {
            Console.Write("Enter genre name: ");
            string genreInput = Console.ReadLine() ?? "";

            selectedGenre = genreInput.ParseEnum<Genre>();

            if (selectedGenre is null)
            {
                Console.WriteLine("Invalid genre. Please try again.\n");
            }
        }

        var filteredFilms = _manager.GetFilmsByGenre(selectedGenre.Value);

        Console.WriteLine($"\nFilms in {selectedGenre.Value.GetDisplayName()} genre:");
        Console.WriteLine("===========================================");

        if (filteredFilms.Any())
        {
            foreach (var film in filteredFilms)
            {
                Console.WriteLine(film.GetInfo());
            }
        }
        else
        {
            Console.WriteLine("No films found in this genre.");
        }
    }

    private void ShowAllActors()
    {
        Console.WriteLine("\nAll actors");
        Console.WriteLine("==========");
        foreach (var actor in _manager.GetActors())
        {
            Console.WriteLine(actor.GetInfo());
        }
    }

    private void ShowActorsWithCriteria()
    {
        Console.Write("Enter (part of) a name or leave blank: ");
        string nameFilter = Console.ReadLine() ?? "";

        Console.Write("Enter minimum age or leave blank: ");
        string ageInput = Console.ReadLine() ?? "";
        int? ageFilter = null;
        if (int.TryParse(ageInput, out int parsedAge))
        {
            ageFilter = parsedAge;
        }

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
    
    private void AddFilm()
    {
        Console.WriteLine("\nAdd film");
        Console.WriteLine("========");

        bool success = false;
        while (!success)
        {
            try
            {
                Console.Write("Title: ");
                string title = Console.ReadLine() ?? "";
                
                Console.WriteLine("\nAvailable genres:");
                foreach (var name in EnumExtensions.GetAllDisplayNames<Genre>())
                {
                    Console.WriteLine($"- {name}");
                }

                Genre? genre = null;
                while (genre is null)
                {
                    Console.Write("Enter genre name: ");
                    string input = Console.ReadLine() ?? "";
                    genre = input.ParseEnum<Genre>();

                    if (genre is null)
                        Console.WriteLine("Invalid genre. Please try again.\n");
                }
                
                Console.Write("Release date (yyyy-MM-dd): ");
                string releaseDateInput = Console.ReadLine() ?? "";
                if (!DateTime.TryParse(releaseDateInput, out var releaseDate))
                {
                    Console.WriteLine("Error: Invalid date format.");
                    Console.WriteLine("Please try again...\n");
                    continue;
                }
                
                Console.Write("Rating (0-10): ");
                string ratingInput = Console.ReadLine() ?? "";
                if (!double.TryParse(ratingInput, out var rating))
                {
                    Console.WriteLine("Error: Invalid rating.");
                    Console.WriteLine("Please try again...\n");
                    continue;
                }
                
                Console.Write("Director name: ");
                string directorInput = Console.ReadLine() ?? "";
                var director = _manager.GetDirectorByName(directorInput);

                if (director == null)
                {
                    Console.Write("Director not found. Country: ");
                    string country = Console.ReadLine() ?? "";

                    Console.Write("Year started (optional): ");
                    string yearInput = Console.ReadLine() ?? "";
                    int? yearStarted = null;
                    if (int.TryParse(yearInput, out var yearS))
                        yearStarted = yearS;
                    
                    Console.Write("Year ended (optional): ");
                    string yearEndedInput = Console.ReadLine() ?? "";
                    int? yearEnded = null;
                    if (int.TryParse(yearEndedInput, out var yearE))
                        yearEnded = yearE;

                    try
                    {
                        director = _manager.AddDirector(directorInput, country, yearStarted, yearEnded);
                        Console.WriteLine("Director created.");
                    }
                    catch (ValidationException ex)
                    {
                        DisplayValidationErrors(ex.Message);
                        Console.WriteLine("Please try again...\n");
                        continue;
                    }
                }
                
                _manager.AddFilm(title, genre.Value, releaseDate, rating, director);
                Console.WriteLine("Film added successfully!");
                success = true;
            }
            catch (ValidationException ex)
            {
                DisplayValidationErrors(ex.Message);
                Console.WriteLine("Please try again...\n");
            }
        }
    }
    
    private void AddActor()
    {
        Console.WriteLine("\nAdd actor");
        Console.WriteLine("=========");
    
        bool success = false;
        while (!success)
        {
            try
            {
                Console.Write("Name: ");
                string name = Console.ReadLine() ?? "";
            
                Console.Write("Nationality: ");
                string nationality = Console.ReadLine() ?? "";
            
                Console.Write("Date of birth (yyyy-MM-dd): ");
                string dateOfBirthInput = Console.ReadLine() ?? "";
                if (!DateTime.TryParse(dateOfBirthInput, out var dateOfBirth))
                {
                    Console.WriteLine("Error: Invalid date format.");
                    Console.WriteLine("Please try again...\n");
                    continue;
                }
                
                Console.Write("Date of death (yyyy-MM-dd) (optional): ");
                string dateOfDeathInput = Console.ReadLine() ?? "";
               
                DateTime? dateOfDeath = null;
                
                if (!string.IsNullOrWhiteSpace(dateOfDeathInput))
                {
                    if (!DateTime.TryParse(dateOfDeathInput, out var parsedDate))
                    {
                        Console.WriteLine("Error: Invalid date format.");
                        Console.WriteLine("Please try again...\n");
                        continue;
                    }
                    dateOfDeath = parsedDate;
                }
                
                _manager.AddActor(name, nationality, dateOfBirth, dateOfDeath);
                Console.WriteLine("Actor added successfully!");
                success = true;
            }
            catch (ValidationException ex)
            {
                DisplayValidationErrors(ex.Message);
                Console.WriteLine("Please try again...\n");
            }
        }
    }
}