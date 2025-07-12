using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TimeControl.Abstractions;
using TimeControl.Core.Models;
using TimeControl.Interfaces;
using TimeControl.Services;

namespace TimeControl.Cases
{
    public class ConsoleCases
    {
        public static ICommand[] UseConsoleCases()
        {
            ICommand[] commands =
            {
                new StartTimeCommand(),
                new StopTimeCommand(),
            };
            return commands;
        }
    }

    public class StartTimeCommand : ICommand
    {
        public string Name => "start";

        public string Description => throw new NotImplementedException();

        public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
        {
            await Task.CompletedTask;
            data.DateNow = DateTime.Now;
        }
    }

    public class StopTimeCommand : ICommand
    {
        public string Name => "stop";

        public string Description => throw new NotImplementedException();

        public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
        {
            try
            {
                var notesService = provider.GetService<INotesWorkService>();
                var notes = NotesWork.Create(data.DateNow, DateTime.Now, "no descriptions");
                if (!string.IsNullOrEmpty(notes.error)) return;
                var result = await notesService!.AddAsync(notes.notesWork!);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);   
            }
        }
    }

    public class HelpCommand : ICommand
    {
        public string Name => "help";

        public string Description => throw new NotImplementedException();

        public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }
    }

    public class VersionCommand : ICommand
    {
        public string Name => "version";

        public string Description => "\n" +
                        "Структура: [command] [argument] \n" +
                        "Отвечает за вывод текущей версии приложения\n" +
                        "Аргументы: \n";

        public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
        {
            await Task.CompletedTask;
            Console.WriteLine("\n" +
                "Версия 1.0.0, developer sergxlove, 2025\n" +
                "Все права защищены\n");
        }
    }

    public class DeveloperCommand : ICommand
    {
        public string Name => "developer";

        public string Description => "\n" +
                        "Структура: [command] [argument] \n" +
                        "Отвечает за вывод информации о разработчике\n" +
                        "Аргументы: \n";

        public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
        {
            await Task.CompletedTask;
            Console.WriteLine("\n" +
                "╔══╗╔═══╗╔═══╗╔═══╗╔══╗╔══╗╔╗──╔══╗╔╗╔╗╔═══╗\n" +
                "║╔═╝║╔══╝║╔═╗║║╔══╝╚═╗║║╔═╝║║──║╔╗║║║║║║╔══╝\n" +
                "║╚═╗║╚══╗║╚═╝║║║╔═╗──║╚╝║──║║──║║║║║║║║║╚══╗\n" +
                "╚═╗║║╔══╝║╔╗╔╝║║╚╗║──║╔╗║──║║──║║║║║╚╝║║╔══╝\n" +
                "╔═╝║║╚══╗║║║║─║╚═╝║╔═╝║║╚═╗║╚═╗║╚╝║╚╗╔╝║╚══╗\n" +
                "╚══╝╚═══╝╚╝╚╝─╚═══╝╚══╝╚══╝╚══╝╚══╝─╚╝─╚═══╝\n");
        }
    }
}
