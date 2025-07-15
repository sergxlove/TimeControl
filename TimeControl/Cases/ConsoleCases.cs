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
                new TaskCommand(),
                new SettingCommand(),
                new TargetsCommand(),
                new InfoCommand()
            };
            return commands;
        }
    }

    public class InfoCommand : ToolsForCommands, ICommand
    {
        public string Name => "?";

        public string Description => "\n" +
                        "Структура: ? [argument] \n" +
                        "Отвечает за вывод подробной информации о команде\n" +
                        "В качестве аргумента используется нужная команда\n";

        public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
        {
            await Task.CompletedTask;
            if (args.Length == 0)
            {
                Console.WriteLine(Description);
            }
            else
            {
                ICommand cmd;
                switch (args[0])
                {
                    case "start":
                        cmd = new StartTimeCommand();
                        break;
                    case "stop":
                        cmd = new StopTimeCommand();
                        break;
                    case "help":
                        cmd = new HelpCommand();
                        break;
                    case "version":
                        cmd = new VersionCommand();
                        break;
                    case "developer":
                        cmd = new DeveloperCommand();
                        break;
                    case "cancel":
                        cmd = new CancelCommand();
                        break;
                    case "tasks":
                        cmd = new TaskCommand();
                        break;
                    case "settings":
                        cmd = new SettingCommand();
                        break;
                    case "targets":
                        cmd = new TargetsCommand();
                        break;
                    case "targets-add":
                        cmd = new TargetsAddCommand();
                        break;
                    case "targets-del":
                        cmd = new TargetsDeleteCommand();
                        break;
                    default:
                        Console.WriteLine("invalid argument");
                        return;
                }
                Console.WriteLine(cmd.Description);
            }
        }
    }

    public class StartTimeCommand : ToolsForCommands, ICommand
    {
        public string Name => "start";

        public string Description => "\n" +
                        "Структура: start [argument] \n" +
                        "Отвечает за запуск задачи\n" +
                        "Аргументы: \n" +
                        "-m [parameter]: указание названия задачи";

        public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
        {
            await Task.CompletedTask;
            if (data.TaskStarted)
            {
                Console.WriteLine($"Task {data.Description} is not stopped");
                return;
            }
            if (args.Length == 0)
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

    public class StopTimeCommand : ToolsForCommands, ICommand
    {
        public string Name => "stop";

        public string Description => "\n" +
                        "Структура: stop \n" +
                        "Отвечает за остановку текущей задачи \n";

        public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
        {
            try
            {
                if (data.TaskStarted)
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

    public class HelpCommand : ToolsForCommands, ICommand
    {
        public string Name => "help";

        public string Description => "\n" +
                        "Структура: help \n" +
                        "Отвечает за вывод информации о доступных командах \n";

        public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
        {
            await Task.CompletedTask;
            string information = "Доступные команды: \n" +
                "? - вывод подробной информации о команде \n" +
                "start - запуск новой задачи \n" +
                "stop - остановка текущей задачи \n" +
                "version - вывод информации о версии программы\n" +
                "developer - вывод информации о разработчике \n" +
                "cancel - отмена  текущей задачи \n" +
                "tasks - вывод информации о задачах\n" +
                "settings - управление настройками программы\n" +
                "targets - вывод информации о целях\n" +
                "targets-add - добавление цели\n" +
                "targets-del - удаление цели\n";
            Console.WriteLine(information);
        }
    }

    public class VersionCommand : ToolsForCommands, ICommand
    {
        public string Name => "version";

        public string Description => "\n" +
                        "Структура: version \n" +
                        "Отвечает за вывод текущей версии приложения\n";

        public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
        {
            await Task.CompletedTask;
            Console.WriteLine("\n" +
                "Версия 1.0.0, developer sergxlove, 2025\n" +
                "Все права защищены\n");
        }
    }

    public class DeveloperCommand : ToolsForCommands, ICommand
    {
        public string Name => "developer";

        public string Description => "\n" +
                        "Структура: developer \n" +
                        "Отвечает за вывод информации о разработчике\n";

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

    public class CancelCommand : ToolsForCommands, ICommand
    {
        public string Name => "cancel";

        public string Description => "\n" +
                        "Структура: cancel \n" +
                        "Отвечает за отмену задачи\n";

        public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
        {
            await Task.CompletedTask;
            if (data.TaskStarted)
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

    public class TaskCommand : ToolsForCommands, ICommand
    {
        public string Name => "tasks";

        public string Description => "\n" +
                        "Структура: tasks [argument] \n" +
                        "Отвечает за управление задачами\n" +
                        "Аргументы: \n" +
                        "-c : указывает на вывод информации о текущей задаче \n" +
                        "-d [parameter]: указывает дату для вывода задача в определенный день\n";

        public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
        {
            await Task.CompletedTask;
            if (args.Length == 0)
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
                    switch (item.Key)
                    {
                        case "-c":
                            if (data.TaskStarted) Console.WriteLine($"Task {data.Description} " +
                                $"started {data.DateStart}");
                            break;
                        case "-d":
                            if (item.Value == string.Empty)
                            {
                                Console.WriteLine("invalid arguments");
                                return;
                            }
                            DateOnly date = DateOnly.Parse(item.Value);
                            var noteService = provider.GetService<INotesWorkService>();
                            var result = await noteService!.GetByDataAsync(date);
                            foreach (var note in result)
                            {
                                Console.WriteLine(note.ToString());
                            }
                            break;
                        default:
                            Console.WriteLine("bad arguments");
                            return;
                    }
                }
            }
        }
    }

    public class SettingCommand : ToolsForCommands, ICommand
    {
        public string Name => "settings";

        public string Description => "\n" +
                        "Структура: [command] [argument] \n" +
                        "Отвечает за настройку программы\n" +
                        "Аргументы: \n";

        public Task Execute(string[] args, DataCore data, ServiceProvider provider)
        {
            throw new NotImplementedException();
        }
    }

    public class TargetsCommand : ToolsForCommands, ICommand
    {
        public string Name => "targets";

        public string Description => "\n" +
                        "Структура: [command] [argument] \n" +
                        "Отвечает за управление задачами\n" +
                        "Аргументы: \n";

        public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
        {
            await Task.CompletedTask;
            if (args.Length == 0)
            {
                Console.WriteLine("bad arguments");
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
                    case "-d":
                        if (item.Value == string.Empty)
                        {
                            Console.WriteLine("invalid arguments");
                            return;
                        }
                        DateOnly date = DateOnly.Parse(item.Value);
                        var targetsService = provider.GetService<ITargetsService>();
                        var result = await targetsService!.GetByDateAsync(date);
                        foreach (var target in result)
                        {
                            Console.WriteLine(target.ToString());
                        }
                        break;
                    default:
                        Console.WriteLine("bad arguments");
                        return;
                }
            }
        }
    }

    public class TargetsAddCommand : ToolsForCommands, ICommand
    {
        public string Name => "targets-add";

        public string Description => "\n" +
                        "Структура: [command] [argument] \n" +
                        "Отвечает за управление задачами\n" +
                        "Аргументы: \n";

        public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }
    }

    public class TargetsDeleteCommand : ToolsForCommands, ICommand
    {
        public string Name => "targets-del";

        public string Description => "\n" +
                        "Структура: [command] [argument] \n" +
                        "Отвечает за управление задачами\n" +
                        "Аргументы: \n";

        public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
        {
            await Task.CompletedTask;
            if (args.Length == 0)
            {
                Console.WriteLine("bad arguments");
                return;
            }
            var argsPairs = ConvertArgsToDictionary(args);
            string description = string.Empty;
            DateOnly date = DateOnly.MinValue;
            foreach (var item in argsPairs)
            {
                switch (item.Key)
                {
                    case "-d":
                        if (item.Value == string.Empty)
                        {
                            Console.WriteLine("invalid arguments");
                            return;
                        }
                        date = DateOnly.Parse(item.Value);
                        break;
                    case "-n":
                        if (item.Value == string.Empty)
                        {
                            Console.WriteLine("invalid arguments");
                            return;
                        }
                        description = item.Value;
                        break;
                    default:
                        Console.WriteLine("bad arguments");
                        return;
                }
            }
            if (description == string.Empty)
            {
                Console.WriteLine("argument description is null");
                return;
            }
            if (date == DateOnly.MinValue)
            {
                Console.WriteLine("argument date is null");
                return;
            }
            var targetService = provider.GetService<ITargetsService>();
            if (!await targetService!.IsHaveAsync(description, date))
            {
                Console.WriteLine("no have object");
            }
            else
            {
                await targetService.DeleteByDescriptionAsync(description);
            }
        }
    }
}
