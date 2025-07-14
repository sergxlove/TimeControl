using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("D:\\projects\\TimeControl\\TimeControl\\appsettings.json")
                .Build();
            string connectionString = config.GetSection("Connection")["ConnectionString"]!;
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
