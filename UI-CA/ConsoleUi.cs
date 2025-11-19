using FilmManagement.BL;
using FilmManagement.UI.CA.Handlers;

namespace FilmManagement.UI.CA;

public class ConsoleUi(IManager manager)
{
    private readonly FilmMenuHandler _filmHandler = new(manager);
    private readonly ActorMenuHandler _actorHandler = new(manager);

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
                    Console.WriteLine("\nGoodbye!");
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

    private void ShowMainMenu()
    {
        Console.WriteLine("\n===================================");
        Console.WriteLine("=== Film Management System Menu ===");
        Console.WriteLine("===================================");
        Console.WriteLine("0) Quit");
        Console.WriteLine("\n--- Films ---");
        Console.WriteLine("1) Show all films");
        Console.WriteLine("2) Show films by genre");
        Console.WriteLine("3) Add a film");
        Console.WriteLine("\n--- Actors ---");
        Console.WriteLine("4) Show all actors");
        Console.WriteLine("5) Show actors with criteria");
        Console.WriteLine("6) Add an actor");
        Console.WriteLine("7) Add an actor to a film");
        Console.WriteLine("8) Remove an actor from a film");
        Console.Write("\nChoice (0-8): ");
    }

    private void HandleMenuChoice(int choice)
    {
        try
        {
            switch (choice)
            {
                case 1:
                    _filmHandler.ShowAllFilms();
                    break;
                case 2:
                    _filmHandler.ShowFilmsByGenre();
                    break;
                case 3:
                    _filmHandler.AddFilm();
                    break;
                case 4:
                    _actorHandler.ShowAllActors();
                    break;
                case 5:
                    _actorHandler.ShowActorsWithCriteria();
                    break;
                case 6:
                    _actorHandler.AddActor();
                    break;
                case 7:
                    _actorHandler.AddActorToFilm();
                    break;
                case 8:
                    _actorHandler.RemoveActorFromFilm();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nAn error occurred: {ex.Message}");
        }
    }
}