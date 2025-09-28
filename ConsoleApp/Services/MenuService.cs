namespace ConsoleApp.Services;

using ConsoleApp.Models;

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
            Console.WriteLine("3) Show films by director");
            Console.WriteLine("4) Show films by genre AND director");
            Console.WriteLine("5) Show films by release year range");
            Console.WriteLine("6) Show films by rating range");
            Console.WriteLine("7) Show films with advanced filters (genre/director/year/rating)");
            Console.WriteLine("8) Show all actors");
            Console.WriteLine("9) Show actors with name and/or age criteria");
            Console.WriteLine("10) Show actors by nationality");
            Console.WriteLine("11) Show actors with film count criteria");
            Console.WriteLine("12) Show all directors");
            Console.WriteLine("13) Show directors by country and/or career span");
            Console.Write("Choice (0-13): ");
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
                    ShowFilmsByDirector();
                    break;
                case 4:
                    ShowFilmsByGenreAndDirector();
                    break;
                case 5:
                    ShowFilmsByYearRange();
                    break;
                case 6:
                    ShowFilmsByRatingRange();
                    break;
                case 7:
                    ShowFilmsWithAdvancedFilters();
                    break;
                case 8:
                    ShowAllActors();
                    break;
                case 9:
                    ShowActorsWithCriteria();
                    break;
                case 10:
                    ShowActorsByNationality();
                    break;
                case 11:
                    ShowActorsByFilmCount();
                    break;
                case 12:
                    ShowAllDirectors();
                    break;
                case 13:
                    ShowDirectorsWithCriteria();
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

        private void ShowFilmsByDirector()
        {
            Console.WriteLine("\nSelect director:");
            for (int i = 0; i < _dataService.Directors.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {_dataService.Directors[i].Name}");
            }
            Console.Write("Director choice (1-{0}): ", _dataService.Directors.Count);
            
            if (int.TryParse(Console.ReadLine(), out int directorChoice) && 
                directorChoice >= 1 && directorChoice <= _dataService.Directors.Count)
            {
                var selectedDirector = _dataService.Directors[directorChoice - 1];
                var filteredFilms = _dataService.Films.Where(f => f.Director.Name == selectedDirector.Name).ToList();
                
                Console.WriteLine($"\nFilms directed by {selectedDirector.Name}:");
                Console.WriteLine("========================================");
                foreach (var film in filteredFilms)
                {
                    Console.WriteLine(film.ToString());
                }
            }
            else
            {
                Console.WriteLine("Invalid director selection.");
            }
        }

        private void ShowFilmsByGenreAndDirector()
        {
            Console.WriteLine("\nSelect genre:");
            var genres = Enum.GetValues<Genre>();
            for (int i = 0; i < genres.Length; i++)
            {
                Console.WriteLine($"{(int)genres[i]}) {genres[i]}");
            }
            Console.Write("Genre choice: ");
            
            if (!int.TryParse(Console.ReadLine(), out int genreChoice) || 
                !Enum.IsDefined(typeof(Genre), genreChoice))
            {
                Console.WriteLine("Invalid genre selection.");
                return;
            }

            Console.WriteLine("\nSelect director:");
            for (int i = 0; i < _dataService.Directors.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {_dataService.Directors[i].Name}");
            }
            Console.Write("Director choice (1-{0}): ", _dataService.Directors.Count);
            
            if (!int.TryParse(Console.ReadLine(), out int directorChoice) || 
                directorChoice < 1 || directorChoice > _dataService.Directors.Count)
            {
                Console.WriteLine("Invalid director selection.");
                return;
            }

            var selectedGenre = (Genre)genreChoice;
            var selectedDirector = _dataService.Directors[directorChoice - 1];
            
            var filteredFilms = _dataService.Films.Where(f => 
                f.Genre == selectedGenre && f.Director.Name == selectedDirector.Name).ToList();
            
            Console.WriteLine($"\n{selectedGenre} films directed by {selectedDirector.Name}:");
            Console.WriteLine("=============================================");
            if (filteredFilms.Any())
            {
                foreach (var film in filteredFilms)
                {
                    Console.WriteLine(film.ToString());
                }
            }
            else
            {
                Console.WriteLine("No films found matching these criteria.");
            }
        }

        private void ShowFilmsByYearRange()
        {
            Console.Write("Enter start year (or leave blank): ");
            string startYearInput = Console.ReadLine() ?? "";
            int? startYear = null;
            if (int.TryParse(startYearInput, out int parsedStartYear))
            {
                startYear = parsedStartYear;
            }

            Console.Write("Enter end year (or leave blank): ");
            string endYearInput = Console.ReadLine() ?? "";
            int? endYear = null;
            if (int.TryParse(endYearInput, out int parsedEndYear))
            {
                endYear = parsedEndYear;
            }

            var filteredFilms = _dataService.Films.Where(film =>
            {
                bool startYearMatch = !startYear.HasValue || film.ReleaseDate.Year >= startYear;
                bool endYearMatch = !endYear.HasValue || film.ReleaseDate.Year <= endYear;
                return startYearMatch && endYearMatch;
            }).OrderBy(f => f.ReleaseDate.Year).ToList();

            var yearRangeText = "";
            if (startYear.HasValue && endYear.HasValue)
                yearRangeText = $"between {startYear} and {endYear}";
            else if (startYear.HasValue)
                yearRangeText = $"from {startYear} onwards";
            else if (endYear.HasValue)
                yearRangeText = $"up to {endYear}";
            else
                yearRangeText = "all years";

            Console.WriteLine($"\nFilms released {yearRangeText}:");
            Console.WriteLine("===============================");
            foreach (var film in filteredFilms)
            {
                Console.WriteLine(film.ToString());
            }
        }

        private void ShowFilmsByRatingRange()
        {
            Console.Write("Enter minimum rating (0-10) or leave blank: ");
            string minRatingInput = Console.ReadLine() ?? "";
            double? minRating = null;
            if (double.TryParse(minRatingInput, out double parsedMinRating))
            {
                minRating = parsedMinRating;
            }

            Console.Write("Enter maximum rating (0-10) or leave blank: ");
            string maxRatingInput = Console.ReadLine() ?? "";
            double? maxRating = null;
            if (double.TryParse(maxRatingInput, out double parsedMaxRating))
            {
                maxRating = parsedMaxRating;
            }

            var filteredFilms = _dataService.Films.Where(film =>
            {
                bool minRatingMatch = !minRating.HasValue || film.Rating >= minRating;
                bool maxRatingMatch = !maxRating.HasValue || film.Rating <= maxRating;
                return minRatingMatch && maxRatingMatch;
            }).OrderByDescending(f => f.Rating).ToList();

            var ratingRangeText = "";
            if (minRating.HasValue && maxRating.HasValue)
                ratingRangeText = $"between {minRating:F1} and {maxRating:F1}";
            else if (minRating.HasValue)
                ratingRangeText = $"above {minRating:F1}";
            else if (maxRating.HasValue)
                ratingRangeText = $"below {maxRating:F1}";
            else
                ratingRangeText = "all ratings";

            Console.WriteLine($"\nFilms with rating {ratingRangeText}:");
            Console.WriteLine("====================================");
            foreach (var film in filteredFilms)
            {
                Console.WriteLine(film.ToString());
            }
        }

        private void ShowFilmsWithAdvancedFilters()
        {
            Console.WriteLine("\nAdvanced Film Search");
            Console.WriteLine("====================");
            
            // Genre filter
            Console.WriteLine("\nSelect genre (or press Enter to skip):");
            var genres = Enum.GetValues<Genre>();
            for (int i = 0; i < genres.Length; i++)
            {
                Console.WriteLine($"{(int)genres[i]}) {genres[i]}");
            }
            Console.Write("Genre choice: ");
            string genreInput = Console.ReadLine() ?? "";
            Genre? selectedGenre = null;
            if (int.TryParse(genreInput, out int genreChoice) && Enum.IsDefined(typeof(Genre), genreChoice))
            {
                selectedGenre = (Genre)genreChoice;
            }

            // Director filter
            Console.WriteLine("\nSelect director (or press Enter to skip):");
            for (int i = 0; i < _dataService.Directors.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {_dataService.Directors[i].Name}");
            }
            Console.Write("Director choice: ");
            string directorInput = Console.ReadLine() ?? "";
            FilmDirector selectedDirector = null;
            if (int.TryParse(directorInput, out int directorChoice) && 
                directorChoice >= 1 && directorChoice <= _dataService.Directors.Count)
            {
                selectedDirector = _dataService.Directors[directorChoice - 1];
            }

            // Year range filter
            Console.Write("\nEnter start year (or leave blank): ");
            string startYearInput = Console.ReadLine() ?? "";
            int? startYear = int.TryParse(startYearInput, out int parsedStartYear) ? parsedStartYear : null;

            Console.Write("Enter end year (or leave blank): ");
            string endYearInput = Console.ReadLine() ?? "";
            int? endYear = int.TryParse(endYearInput, out int parsedEndYear) ? parsedEndYear : null;

            // Rating range filter
            Console.Write("\nEnter minimum rating (or leave blank): ");
            string minRatingInput = Console.ReadLine() ?? "";
            double? minRating = double.TryParse(minRatingInput, out double parsedMinRating) ? parsedMinRating : null;

            Console.Write("Enter maximum rating (or leave blank): ");
            string maxRatingInput = Console.ReadLine() ?? "";
            double? maxRating = double.TryParse(maxRatingInput, out double parsedMaxRating) ? parsedMaxRating : null;

            // Apply all filters
            var filteredFilms = _dataService.Films.Where(film =>
            {
                bool genreMatch = !selectedGenre.HasValue || film.Genre == selectedGenre;
                bool directorMatch = selectedDirector == null || film.Director.Name == selectedDirector.Name;
                bool startYearMatch = !startYear.HasValue || film.ReleaseDate.Year >= startYear;
                bool endYearMatch = !endYear.HasValue || film.ReleaseDate.Year <= endYear;
                bool minRatingMatch = !minRating.HasValue || film.Rating >= minRating;
                bool maxRatingMatch = !maxRating.HasValue || film.Rating <= maxRating;

                return genreMatch && directorMatch && startYearMatch && 
                       endYearMatch && minRatingMatch && maxRatingMatch;
            }).OrderByDescending(f => f.Rating).ToList();

            // Build filter description
            var filters = new List<string>();
            if (selectedGenre.HasValue) filters.Add($"Genre: {selectedGenre}");
            if (selectedDirector != null) filters.Add($"Director: {selectedDirector.Name}");
            if (startYear.HasValue || endYear.HasValue)
            {
                if (startYear.HasValue && endYear.HasValue)
                    filters.Add($"Years: {startYear}-{endYear}");
                else if (startYear.HasValue)
                    filters.Add($"Years: {startYear}+");
                else
                    filters.Add($"Years: up to {endYear}");
            }
            if (minRating.HasValue || maxRating.HasValue)
            {
                if (minRating.HasValue && maxRating.HasValue)
                    filters.Add($"Rating: {minRating:F1}-{maxRating:F1}");
                else if (minRating.HasValue)
                    filters.Add($"Rating: {minRating:F1}+");
                else
                    filters.Add($"Rating: up to {maxRating:F1}");
            }

            Console.WriteLine($"\nFilms matching criteria [{string.Join(", ", filters)}]:");
            Console.WriteLine("=======================================================");
            if (filteredFilms.Any())
            {
                foreach (var film in filteredFilms)
                {
                    Console.WriteLine(film.ToString());
                }
                Console.WriteLine($"\nTotal: {filteredFilms.Count} film(s) found.");
            }
            else
            {
                Console.WriteLine("No films found matching all criteria.");
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

        private void ShowActorsByNationality()
        {
            Console.Write("Enter nationality (or part of it): ");
            string nationalityFilter = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(nationalityFilter))
            {
                Console.WriteLine("Please enter a nationality to search for.");
                return;
            }

            var filteredActors = _dataService.Actors.Where(actor =>
                actor.Nationality.Contains(nationalityFilter, StringComparison.OrdinalIgnoreCase)
            ).OrderBy(a => a.Nationality).ThenBy(a => a.Name).ToList();

            Console.WriteLine($"\nActors with nationality containing '{nationalityFilter}':");
            Console.WriteLine("========================================================");
            if (filteredActors.Any())
            {
                foreach (var actor in filteredActors)
                {
                    Console.WriteLine(actor.ToString());
                }
            }
            else
            {
                Console.WriteLine("No actors found with that nationality.");
            }
        }

        private void ShowActorsByFilmCount()
        {
            Console.Write("Enter minimum number of films (or leave blank): ");
            string minFilmsInput = Console.ReadLine() ?? "";
            int? minFilms = null;
            if (int.TryParse(minFilmsInput, out int parsedMinFilms))
            {
                minFilms = parsedMinFilms;
            }

            Console.Write("Enter maximum number of films (or leave blank): ");
            string maxFilmsInput = Console.ReadLine() ?? "";
            int? maxFilms = null;
            if (int.TryParse(maxFilmsInput, out int parsedMaxFilms))
            {
                maxFilms = parsedMaxFilms;
            }

            var filteredActors = _dataService.Actors.Where(actor =>
            {
                int filmCount = actor.Films.Count;
                bool minMatch = !minFilms.HasValue || filmCount >= minFilms;
                bool maxMatch = !maxFilms.HasValue || filmCount <= maxFilms;
                return minMatch && maxMatch;
            }).OrderByDescending(a => a.Films.Count).ThenBy(a => a.Name).ToList();

            var filmCountText = "";
            if (minFilms.HasValue && maxFilms.HasValue)
                filmCountText = $"between {minFilms} and {maxFilms} films";
            else if (minFilms.HasValue)
                filmCountText = $"at least {minFilms} film(s)";
            else if (maxFilms.HasValue)
                filmCountText = $"at most {maxFilms} film(s)";
            else
                filmCountText = "any number of films";

            Console.WriteLine($"\nActors with {filmCountText}:");
            Console.WriteLine("=============================");
            foreach (var actor in filteredActors)
            {
                Console.WriteLine(actor.ToString());
            }
        }

        private void ShowAllDirectors()
        {
            Console.WriteLine("\nAll directors");
            Console.WriteLine("=============");
            foreach (var director in _dataService.Directors.OrderBy(d => d.Name))
            {
                Console.WriteLine(director.ToString());
            }
        }

        private void ShowDirectorsWithCriteria()
        {
            Console.Write("Enter country (or part of it, or leave blank): ");
            string countryFilter = Console.ReadLine() ?? "";

            Console.Write("Enter minimum career start year (or leave blank): ");
            string minYearInput = Console.ReadLine() ?? "";
            int? minCareerYear = null;
            if (int.TryParse(minYearInput, out int parsedMinYear))
            {
                minCareerYear = parsedMinYear;
            }

            Console.Write("Enter maximum career start year (or leave blank): ");
            string maxYearInput = Console.ReadLine() ?? "";
            int? maxCareerYear = null;
            if (int.TryParse(maxYearInput, out int parsedMaxYear))
            {
                maxCareerYear = parsedMaxYear;
            }

            var filteredDirectors = _dataService.Directors.Where(director =>
            {
                bool countryMatch = string.IsNullOrEmpty(countryFilter) || 
                                   director.Country.Contains(countryFilter, StringComparison.OrdinalIgnoreCase);
                
                bool minYearMatch = !minCareerYear.HasValue || 
                                   director.CareerStart.Year >= minCareerYear;
                
                bool maxYearMatch = !maxCareerYear.HasValue || 
                                   director.CareerStart.Year <= maxCareerYear;

                return countryMatch && minYearMatch && maxYearMatch;
            }).OrderBy(d => d.CareerStart.Year).ThenBy(d => d.Name).ToList();

            var filters = new List<string>();
            if (!string.IsNullOrEmpty(countryFilter)) filters.Add($"Country: {countryFilter}");
            if (minCareerYear.HasValue || maxCareerYear.HasValue)
            {
                if (minCareerYear.HasValue && maxCareerYear.HasValue)
                    filters.Add($"Career: {minCareerYear}-{maxCareerYear}");
                else if (minCareerYear.HasValue)
                    filters.Add($"Career: {minCareerYear}+");
                else
                    filters.Add($"Career: up to {maxCareerYear}");
            }

            var filterText = filters.Any() ? $" [{string.Join(", ", filters)}]" : "";
            Console.WriteLine($"\nDirectors{filterText}:");
            Console.WriteLine("=======================");
            foreach (var director in filteredDirectors)
            {
                Console.WriteLine(director.ToString());
            }
        }
    }
