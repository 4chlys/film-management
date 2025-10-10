using FilmManagement.BL;
using FilmManagement.DAL;
using FilmManagement.UI;

DummyDataSeeder.Seed();

IRepository repository = new Repository();
IManager manager = new Manager(repository);
ConsoleUi consoleUi = new ConsoleUi(manager);

Console.WriteLine("Welcome to Film Management System!");
    
bool running = true;
while (running)
{
    consoleUi.ShowMainMenu();
        
    if (int.TryParse(Console.ReadLine(), out int choice))
    {
        if (choice == 0)
        {
            running = false;
            Console.WriteLine("Goodbye!");
        }
        else
        {
            consoleUi.HandleMenuChoice(choice);
        }
    }
    else
    {
        Console.WriteLine("Please enter a valid number.");
    }
}