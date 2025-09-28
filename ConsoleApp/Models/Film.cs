namespace ConsoleApp.Models;
    
    public class Film
    {
        private string _title = string.Empty;
        private Genre _genre;
        private DateTime _releaseDate;
        private double _rating;
        private FilmDirector _director = null!;
        private List<Actor> _actors = new List<Actor>();

        public string Title 
        { 
            get { return _title; } 
            set { _title = value; } 
        }
        
        public Genre Genre 
        { 
            get { return _genre; } 
            set { _genre = value; } 
        }
        
        public DateTime ReleaseDate 
        { 
            get { return _releaseDate; } 
            set { _releaseDate = value; } 
        }
        
        public double Rating 
        { 
            get { return _rating; } 
            set { _rating = value; } 
        }
        
        public FilmDirector Director 
        { 
            get { return _director; } 
            set { _director = value; } 
        }
        
        public List<Actor> Actors 
        { 
            get { return _actors; } 
            set { _actors = value; } 
        }

        public override string ToString()
        {
            var actorNames = string.Join(", ", Actors.Select(a => a.Name));
            return $"{Title} ({ReleaseDate.Year}) [{Genre}] - Rating: {Rating:F1} - Directed by {Director.Name} - Starring: {actorNames}";
        }
    }