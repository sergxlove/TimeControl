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

namespace TimeControl
{
    public class Program
    {
        static void Main(string[] args)
        {
            var defaultConfig = new AppConfig();
            string pathAppsetings = defaultConfig.PathAppsettings;
            var optionsJson = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            if (!File.Exists(pathAppsetings))
            {
                string json = JsonSerializer.Serialize(defaultConfig, optionsJson);
                Console.WriteLine(json);
                File.WriteAllText(pathAppsetings, json);
            }
            else
            {
                string json = File.ReadAllText(pathAppsetings);
                defaultConfig = JsonSerializer.Deserialize<AppConfig>(json);
                if (defaultConfig is null) defaultConfig = new AppConfig();
            }
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(defaultConfig.PathAppsettings)
                .Build();
            string connectionString = config.GetSection("Connection")["ConnectionString"]!;
            string nickname = config.GetSection("")[""]!;
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<TimeControlDbContext>(options => 
                options.UseSqlite(connectionString));
            serviceCollection.AddSingleton<INotesWorkRepository, NotesWorkRepository>();
            serviceCollection.AddSingleton<INotesWorkService, NotesWorkService>();
            serviceCollection.AddSingleton<ITargetsRepository, TargetsRepository>();
            serviceCollection.AddSingleton<ITargetsService, TargetsService>();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
                
            ExecuteCommandCore cmd = new ExecuteCommandCore();
            DataCore data = new DataCore();
            cmd.AddRange(ConsoleCases.UseConsoleCases());
            string commandLine = string.Empty;
            bool exit = false;
            while(!exit)
            {
                Console.Write("users > ");
                commandLine = Console.ReadLine()!;
                if (commandLine == "exit") exit = true;
                cmd.ExecuteCommand(commandLine, data, serviceProvider);
            }
        }
    }
}
