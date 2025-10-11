using System.ComponentModel.DataAnnotations;

namespace FilmManagement.DAL;

public static class ImdbIdGenerator
{
    private static int _actorCounter = 1;
    private static int _filmCounter = 1;
    private static int _directorCounter = 1;
    
    private static readonly Lock _lockObject = new Lock();
    
    public static string GenerateActorId()
    {
        lock (_lockObject)
        {
            return $"nm{_actorCounter++:D7}";
        }
    }
    
    public static string GenerateFilmId()
    {
        lock (_lockObject)
        {
            return $"tt{_filmCounter++:D7}";
        }
    }
    
    public static string GenerateDirectorId()
    {
        lock (_lockObject)
        {
            return $"nm{_directorCounter++:D7}";
        }
    }
    
    public static void Reset()
    {
        lock (_lockObject)
        {
            _actorCounter = 1;
            _filmCounter = 1;
            _directorCounter = 1;
        }
    }
    
    public static void InitializeFromExisting(
        IEnumerable<string> existingActorIds,
        IEnumerable<string> existingFilmIds,
        IEnumerable<string> existingDirectorIds)
    {
        lock (_lockObject)
        {
            _actorCounter = GetNextCounter(existingActorIds, "nm");
            _filmCounter = GetNextCounter(existingFilmIds, "tt");
            _directorCounter = GetNextCounter(existingDirectorIds, "nm");
        }
    }

    private static int GetNextCounter(IEnumerable<string> existingIds, string prefix)
    {
        if (existingIds == null || !existingIds.Any())
            return 1;

        var maxId = existingIds
            .Where(id => id?.StartsWith(prefix) == true && id.Length == 10)
            .Select(id => int.TryParse(id.Substring(2), out int num) ? num : 0)
            .DefaultIfEmpty(0)
            .Max();

        return maxId + 1;
    }
    
    public static bool IsValidImdbId(string imdbId, string expectedPrefix)
    {
        if (string.IsNullOrEmpty(imdbId) || imdbId.Length != 10)
            return false;

        if (!imdbId.StartsWith(expectedPrefix))
            return false;

        return imdbId.Substring(2).All(char.IsDigit);
    }
}