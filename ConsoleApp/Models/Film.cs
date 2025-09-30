namespace ConsoleApp.Models;

public class Film
{
    private readonly List<Actor> _actors = new();

    public string Title { get; set; } = string.Empty;
    public Genre Genre { get; set; }
    public DateTime ReleaseDate { get; set; }
    public double Rating { get; set; } 
    public FilmDirector Director { get; set; }
    
    public ICollection<Actor> Actors => _actors.AsReadOnly();
    
    public void AddActor(Actor actor) => _actors.Add(actor);

    public override string ToString()
    {
        var actorNames = string.Join(", ", Actors.Select(a => a.Name));
        var actorsText = string.IsNullOrEmpty(actorNames) ? "No actors listed" : $"Starring: {actorNames}";
        return $"{Title} ({ReleaseDate.Year}) [{Genre}] - Rating: {Rating:F1} - Directed by {Director?.Name ?? "Unknown"} - {actorsText}";
    }
}