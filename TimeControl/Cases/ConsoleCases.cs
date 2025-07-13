using Microsoft.Extensions.DependencyInjection;
using TimeControl.Abstractions;
using TimeControl.Core.Models;
using TimeControl.Interfaces;

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
                new HelpCommand(),
                new VersionCommand(),
                new DeveloperCommand(),
                new CancelCommand(),
                new TaskCommand()
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
            if(data.TaskStarted)
            {
                Console.WriteLine($"Task {data.Description} is not stopped");
                return;
            }
            if(args.Length == 0)
            {
                Console.WriteLine("error: arguments is empty");
                return;
            }
            Dictionary<string, string> argsPairs = new Dictionary<string, string>();
            bool isKey = true;
            string keyCash = string.Empty;
            for (int i = 0; i < args.Length; i++)
            {
                if (isKey)
                {
                    argsPairs.Add(args[i], string.Empty);
                    keyCash = args[i];
                    isKey = false;
                }
                else
                {
                    argsPairs[keyCash] = args[i];
                    isKey = true;
                }
            }
            foreach (var item in argsPairs)
            {
                switch (item.Key)
                {
                    case "-m":
                        if (item.Value is null)
                        {
                            Console.WriteLine("invalid arguments");
                            return;
                        }
                        data.Description = item.Value!;
                        break;
                    default:
                        Console.WriteLine($"bad arguments : {item.Key}");
                        return;
                }
            }
            data.DateStart = DateTime.Now;
            data.TaskStarted = true;
            Console.WriteLine($"Task: {data.Description} started {data.DateStart} ");
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
                if(data.TaskStarted)
                {
                    var notesService = provider.GetService<INotesWorkService>();
                    var notes = NotesWork.Create(data.DateStart, DateTime.Now, data.Description);
                    if (!string.IsNullOrEmpty(notes.error)) return;
                    var result = await notesService!.AddAsync(notes.notesWork!);
                    Console.WriteLine($"Task {data.Description} stoped {DateTime.Now}");
                    data.Clear();
                }
                else
                {
                    Console.WriteLine("No started task");
                }
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

    public class CancelCommand : ICommand
    {
        public string Name => "cancel";

        public string Description => "\n" +
                        "Структура: [command] [argument] \n" +
                        "Отвечает за отмену задачи\n" +
                        "Аргументы: \n";

        public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
        {
            await Task.CompletedTask;
            if(data.TaskStarted)
            {
                Console.WriteLine($"Task {data.Description} cancelled");
                data.Clear();
            }
            else
            {
                Console.WriteLine("Task is dont start");
            }
        }
    }

    public class TaskCommand : ICommand
    {
        public string Name => "tasks";

        public string Description => "\n" +
                        "Структура: [command] [argument] \n" +
                        "Отвечает за управление задачами\n" +
                        "Аргументы: \n";

        public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
        {
            await Task.CompletedTask;
            if(args.Length == 0)
            {
                var notesService = provider.GetService<INotesWorkService>();
                DateTime dateTimeNow = DateTime.Now;
                var result = await notesService!.GetByDataAsync(new DateOnly
                    (dateTimeNow.Year, dateTimeNow.Month, dateTimeNow.Day));
                foreach (var note in result)
                {
                    Console.WriteLine(note.ToString());
                }
            }
            else
            {
                Dictionary<string, string> argsPairs = new Dictionary<string, string>();
                bool isKey = true;
                string keyCash = string.Empty;
                for (int i = 0; i < args.Length; i++)
                {
                    if (isKey)
                    {
                        argsPairs.Add(args[i], string.Empty);
                        keyCash = args[i];
                        isKey = false;
                    }
                    else
                    {
                        argsPairs[keyCash] = args[i];
                        isKey = true;
                    }
                }
                foreach (var item in argsPairs)
                {
                    switch(item.Key)
                    {
                        case "-c":
                            if(data.TaskStarted) Console.WriteLine($"Task {data.Description} " +
                                $"started {data.DateStart}");
                            break;
                        case "-d":
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
