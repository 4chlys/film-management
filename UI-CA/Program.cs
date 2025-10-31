using FilmManagement.BL;
using FilmManagement.DAL;
using FilmManagement.DAL.EF;
using FilmManagement.UI.CA;
using Microsoft.EntityFrameworkCore;

string solutionRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"../../../.."));
string dbPath = Path.Combine(solutionRoot, "filmmanagement.db");

string dbDirectory = Path.GetDirectoryName(dbPath);
if (!Directory.Exists(dbDirectory))
{
    Directory.CreateDirectory(dbDirectory ?? string.Empty);
}

string connectionString = $"Data source={dbPath}";

DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
optionsBuilder.UseSqlite(connectionString);
FilmDbContext context = new FilmDbContext(optionsBuilder.Options);

if (context.CreateDatabase(dropDatabase: true))
{
    DataSeeder.Seed(context);
}

IRepository filmRepository = new EfRepository(context);
IManager filmManager = new Manager(filmRepository);
ConsoleUi consoleUi = new ConsoleUi(filmManager);

consoleUi.Run();

