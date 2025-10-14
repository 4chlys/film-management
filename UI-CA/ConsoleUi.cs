using FilmManagement.BL;
using FilmManagement.BL.Domain;
using FilmManagement.DAL;

namespace FilmManagement.UI.CA;

public class ConsoleUi(IManager manager)
{
    private IManager _manager = manager;

    public void ShowMainMenu()
    {
        Console.WriteLine("\nWhat would you like to do?");
        Console.WriteLine("==========================");
        Console.WriteLine("0) Quit");
        Console.WriteLine("1) Show all films");
        Console.WriteLine("2) Show films by genre");
        Console.WriteLine("3) Show all actors");
        Console.WriteLine("4) Show actors with name and/or age");
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
            Console.WriteLine(film.ToString());
        }
    }

    private void ShowFilmsByGenre()
    {
        Console.WriteLine("\nSelect genre:");
        var genres = Enum.GetValues<Genre>();
        foreach (var g in genres)
        {
            Console.WriteLine($"{(int)g}) {g}");
        }
        Console.Write("Genre choice: ");
        
        if (int.TryParse(Console.ReadLine(), out int genreChoice) && 
            Enum.IsDefined(typeof(Genre), genreChoice))
        {
            var selectedGenre = (Genre)genreChoice;
            var filteredFilms = _manager.GetFilmsByGenre(selectedGenre);
            
            Console.WriteLine($"\nFilms in {selectedGenre} genre:");
            Console.WriteLine("===============================");
            foreach (var film in filteredFilms)
            {
                Console.WriteLine(film.ToString());
            }
        }
        else
        {
            Console.WriteLine("Invalid genre selection.");
        }
    }

    private void ShowAllActors()
    {
        Console.WriteLine("\nAll actors");
        Console.WriteLine("==========");
        foreach (var actor in _manager.GetActors())
        {
            Console.WriteLine(actor.ToString());
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

        var filteredActors = _manager.GetActors().Where(actor =>
        {
            bool nameMatch = string.IsNullOrEmpty(nameFilter) || 
                             actor.Name.Contains(nameFilter, StringComparison.OrdinalIgnoreCase);
            
            bool ageMatch = !ageFilter.HasValue || 
                            (actor.Age.HasValue && actor.Age >= ageFilter);

            return nameMatch && ageMatch;
        }).ToList();

        Console.WriteLine($"\nFiltered actors:");
        Console.WriteLine("================");
        if (filteredActors.Any())
        {
            foreach (var actor in filteredActors)
            {
                Console.WriteLine(actor.ToString());
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
                
                Console.WriteLine("Genre:");
                var genres = Enum.GetValues<Genre>();
                foreach (var g in genres)
                {
                    Console.WriteLine($"  {(int)g}) {g}");
                }
                Console.Write("Genre choice: ");
                string genreInput = Console.ReadLine() ?? "";
                if (!int.TryParse(genreInput, out int genreInt) || !Enum.IsDefined(typeof(Genre), genreInt))
                {
                    Console.WriteLine("Error: Invalid genre selection.");
                    Console.WriteLine("Please try again...\n");
                    continue;
                }
                Genre genre = (Genre)genreInt;
                
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
                
                var director = _manager.GetDirectors().FirstOrDefault(d => 
                    d.Name.Equals(directorInput, StringComparison.OrdinalIgnoreCase));
                
                if (director == null)
                {
                    Console.Write("Director not found. Country: ");
                    string country = Console.ReadLine() ?? "";
                    
                    Console.Write("Year started (optional): ");
                    string yearInput = Console.ReadLine() ?? "";
                    int? yearStarted = null;
                    if (int.TryParse(yearInput, out int year))
                    {
                        yearStarted = year;
                    }
                    
                    try
                    {
                        director = _manager.AddDirector(directorInput, country, yearStarted);
                        Console.WriteLine("Director created.");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error creating director: {ex.Message}");
                        Console.WriteLine("Please try again...\n");
                        continue;
                    }
                }
                
                _manager.AddFilm(title, genre, releaseDate, rating, director);
                Console.WriteLine("Film added successfully!");
                success = true;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
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
                
                Console.Write("Age (optional, leave blank to auto-calculate): ");
                string ageInput = Console.ReadLine() ?? "";
                int? age = null;
                if (!string.IsNullOrWhiteSpace(ageInput))
                {
                    if (int.TryParse(ageInput, out int parsedAge))
                    {
                        age = parsedAge;
                    }
                    else
                    {
                        Console.WriteLine("Error: Invalid age.");
                        Console.WriteLine("Please try again...\n");
                        continue;
                    }
                }
                
                _manager.AddActor(name, nationality, dateOfBirth, age);
                Console.WriteLine("Actor added successfully!");
                success = true;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Please try again...\n");
            }
        }
    }
}