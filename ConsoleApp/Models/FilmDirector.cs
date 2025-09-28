namespace ConsoleApp.Models;

    public class FilmDirector
    {
        private string _name = string.Empty;
        private string _country = string.Empty;
        private DateTime _careerStart;
        private List<Film> _films = new List<Film>();

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

        public DateTime CareerStart
        {
            get { return _careerStart; }
            set { _careerStart = value; }
        }

        public List<Film> Films
        {
            get { return _films; }
            set { _films = value; }
        }

        public override string ToString()
        {
            var filmCount = Films.Count;
            var yearsActive = DateTime.Now.Year - CareerStart.Year;
            return
                $"{Name} from {Country}, directing since {CareerStart.Year} ({yearsActive} years) - {filmCount} film(s)";
        }
    }