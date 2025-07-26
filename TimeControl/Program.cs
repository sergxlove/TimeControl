using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using System.Text.Json;
using TimeControl.Abstractions;
using TimeControl.Cases;
using TimeControl.DataAccess.Sqlite;
using TimeControl.DataAccess.Sqlite.Abstractions;
using TimeControl.DataAccess.Sqlite.Repositories;
using TimeControl.Services;
using Serilog;

namespace TimeControl
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new AppConfig();
            string pathAppsetings = config.PathAppsettings;
            var optionsJson = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            if (!File.Exists(pathAppsetings))
            {
                config.CreateConfig();
            }
            else
            {
                config = config.ReadConfig();
                if (config is null) config = new AppConfig();
            }
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(config.PathLog)
                .CreateLogger();
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<TimeControlDbContext>(options => 
                options.UseSqlite(config.ConnectionString));
            serviceCollection.AddSingleton<INotesWorkRepository, NotesWorkRepository>();
            serviceCollection.AddSingleton<INotesWorkService, NotesWorkService>();
            serviceCollection.AddSingleton<ITargetsRepository, TargetsRepository>();
            serviceCollection.AddSingleton<ITargetsService, TargetsService>();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            ExecuteCommandCore cmd = new ExecuteCommandCore();
            DataCore data = new DataCore(config, Log.Logger);
            cmd.AddRange(ConsoleCases.UseConsoleCases());
            string commandLine = string.Empty;
            bool exit = false;
            Log.Logger.Information("app starting");
            while(!exit)
            {
                Console.Write($"{config.Username} > ");
                commandLine = Console.ReadLine()!;
                if (commandLine == "exit") exit = true;
                cmd.ExecuteCommand(commandLine, data, serviceProvider);
            }
            Log.Logger.Information("app stopping");
        }
    }
}
