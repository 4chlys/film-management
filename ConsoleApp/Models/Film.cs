namespace ConsoleApp.Models;

public class Film
{
    private readonly List<Actor> _actors = new();

    public string Title { get; set; } = string.Empty;
    public Genre Genre { get; set; }
    public DateTime ReleaseDate { get; set; }
    public double Rating { get; set; }
    public FilmDirector Director { get; set; }
    
    public Film(string title, FilmDirector director)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Director = director ?? throw new ArgumentNullException(nameof(director));
    }
    
    public IReadOnlyList<Actor> Actors => _actors.AsReadOnly();
    
    public void AddActor(Actor actor) => _actors.Add(actor);
    public void RemoveActor(Actor actor) => _actors.Remove(actor);
    public void ClearActors() => _actors.Clear();
    
    public void AddActorsRange(IEnumerable<Actor> actors) => _actors.AddRange(actors);

    public override string ToString()
    {
        var actorNames = string.Join(", ", Actors.Select(a => a.Name));
        return $"{Title} ({ReleaseDate.Year}) [{Genre}] - Rating: {Rating:F1} - Directed by {Director?.Name ?? "Unknown"} - Starring: {actorNames}";
    }
}