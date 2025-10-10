using ConsoleApp.Services;

var dataService = new DataService();
var menuService = new MenuService(dataService);

dataService.Seed();

Console.WriteLine("Welcome to Film Management System!");
    
bool running = true;
while (running)
{
    menuService.ShowMainMenu();
        
    if (int.TryParse(Console.ReadLine(), out int choice))
    {
        if (choice == 0)
        {
            running = false;
            Console.WriteLine("Goodbye!");
        }
        else
        {
            menuService.HandleMenuChoice(choice);
        }
    }
    else
    {
        Console.WriteLine("Please enter a valid number.");
    }
}