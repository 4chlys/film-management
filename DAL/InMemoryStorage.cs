using FilmManagement.BL.Domain;

namespace FilmManagement.DAL;

internal static class InMemoryStorage
{
    public static List<Film> Films { get; } = [];
    public static List<Actor> Actors { get; } = [];
    public static List<FilmDirector> FilmDirectors { get; } = [];
}