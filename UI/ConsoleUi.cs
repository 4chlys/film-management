
using FilmManagement.BL;
using FilmManagement.BL.Domain;
using FilmManagement.DAL;

namespace FilmManagement.UI;

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
        Console.Write("Choice (0-4): ");
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
            var filteredFilms = _manager.GetFilms().Where(f => f.Genre == selectedGenre).ToList();
            
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
}