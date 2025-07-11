using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeControl.Cases;
using TimeControl.DataAccess.Sqlite;

namespace TimeControl
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("")
                .Build();
            string connectionString = config.GetSection("Connection:ConnectionString").ToString()!;
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<TimeControlDbContext>(options => 
                options.UseSqlite(connectionString));
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
                if (commandLine == "exit") break;
                cmd.ExecuteCommand(commandLine, data);
            }
        }
    }
}
