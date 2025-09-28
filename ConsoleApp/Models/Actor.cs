namespace ConsoleApp.Models;

    public class Actor
    {
        private string _name = string.Empty;
        private string _nationality = string.Empty;
        private DateTime _dateOfBirth;
        private int? _age; // nullable integer
        private List<Film> _films = new List<Film>();

        public string Name 
        { 
            get { return _name; } 
            set { _name = value; } 
        }
        
        public string Nationality 
        { 
            get { return _nationality; } 
            set { _nationality = value; } 
        }
        
        public DateTime DateOfBirth 
        { 
            get { return _dateOfBirth; } 
            set { _dateOfBirth = value; } 
        }
        
        public int? Age 
        { 
            get { return _age; } 
            set { _age = value; } 
        }
        
        public List<Film> Films 
        { 
            get { return _films; } 
            set { _films = value; } 
        }

        public override string ToString()
        {
            var ageDisplay = Age.HasValue ? $" (age {Age})" : "";
            var filmCount = Films.Count;
            return $"{Name} from {Nationality}, born {DateOfBirth:yyyy-MMM-dd}{ageDisplay} - {filmCount} film(s)";
        }
    }
