using ConsoleApp.Models;

namespace ConsoleApp.Services;

public class MenuService
{
        private readonly DataService _dataService;

        public MenuService(DataService dataService)
        {
            _dataService = dataService;
        }

        public void ShowMainMenu()
        {
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("==========================");
            Console.WriteLine("0) Quit");
            Console.WriteLine("1) Show all films");
            Console.WriteLine("2) Show films by genre");
            Console.WriteLine("3) Show all actors");
            Console.WriteLine("4) Show actors with name and/or age criteria");
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
            foreach (var film in _dataService.Films)
            {
                Console.WriteLine(film.ToString());
            }
        }

        private void ShowFilmsByGenre()
        {
            Console.WriteLine("\nSelect genre:");
            var genres = Enum.GetValues<Genre>();
            for (int i = 0; i < genres.Length; i++)
            {
                Console.WriteLine($"{(int)genres[i]}) {genres[i]}");
            }
            Console.Write("Genre choice: ");
            
            if (int.TryParse(Console.ReadLine(), out int genreChoice) && 
                Enum.IsDefined(typeof(Genre), genreChoice))
            {
                var selectedGenre = (Genre)genreChoice;
                var filteredFilms = _dataService.Films.Where(f => f.Genre == selectedGenre).ToList();
                
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
            foreach (var actor in _dataService.Actors)
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

            var filteredActors = _dataService.Actors.Where(actor =>
            {
                bool nameMatch = string.IsNullOrEmpty(nameFilter) || 
                                actor.Name.Contains(nameFilter, StringComparison.OrdinalIgnoreCase);
                
                bool ageMatch = !ageFilter.HasValue || 
                               (actor.Age.HasValue && actor.Age >= ageFilter);

                return nameMatch && ageMatch;
            }).ToList();

            Console.WriteLine($"\nFiltered actors:");
            Console.WriteLine("================");
            foreach (var actor in filteredActors)
            {
                Console.WriteLine(actor.ToString());
            }
        }
    }