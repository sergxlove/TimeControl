using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Runtime.InteropServices;
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
                new TargetsAddCommand(),
                new TargetsDeleteCommand(),
                new InfoCommand()
            };
            return commands;
        }
    }

    public class InfoCommand : ToolsForCommands, ICommand
    {
        public string Name => "?";

        public string Description => "\n" +
            "Структура: ? [Аргумент] \n" +
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
                        Console.WriteLine($"Неизвестный аргумент: {args[0]}");
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
            "Структура: start [Аргумент] \n" +
            "Отвечает за запуск задачи\n" +
            "Аргументы: \n" +
            "-m [Параметр]: указание названия задачи";

        public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
        {
            try
            {
                await Task.CompletedTask;
                if (data.TaskStarted)
                {
                    Console.WriteLine($"Задача {data.Description} не остановлена." +
                        $" Что бы начать новую задачу необходимо остановить старую с помощью команды stop");
                    return;
                }
                if (args.Length == 0)
                {
                    Console.WriteLine("Необходимо указать аргументы. Для получения подробной " +
                        "информации воспользуйтесь командой: ? start ");
                    return;
                }
                Dictionary<string, string> argsPairs = ConvertArgsToDictionary(args);
                foreach (var item in argsPairs)
                {
                    switch (item.Key)
                    {
                        case "-m":
                            if (item.Value is null)
                            {
                                Console.WriteLine("Необходимо ввести параметр");
                                return;
                            }
                            data.Description = item.Value!;
                            break;
                        default:
                            Console.WriteLine($"Неизвестный аргумент : {item.Key}");
                            return;
                    }
                }
                data.DateStart = DateTime.Now;
                data.TaskStarted = true;
                Console.WriteLine($"Задача: {data.Description} запущена {data.DateStart} ");
            }
            catch
            {
                Console.WriteLine("Произошла ошибка");
            }

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
                    Console.WriteLine($"Задача {data.Description} остановлена {DateTime.Now}");
                    data.Clear();
                }
                else
                {
                    Console.WriteLine("Нет запущенных задач");
                }
            }
            catch
            {
                Console.WriteLine("Произошла ошибка");
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
                Console.WriteLine($"Задача {data.Description} отменена");
                data.Clear();
            }
            else
            {
                Console.WriteLine("Нет запущенных задач");
            }
        }
    }

    public class TaskCommand : ToolsForCommands, ICommand
    {
        public string Name => "tasks";

        public string Description => "\n" +
            "Структура: tasks [Аргументы] \n" +
            "Отвечает за управление задачами\n" +
            "Аргументы: \n" +
            "-c : указывает на вывод информации о текущей задаче \n" +
            "-d [Параметр]: указывает дату для вывода задача в определенный день\n";

        public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
        {
            try
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
                    return;
                }
                Dictionary<string, string> argsPairs = ConvertArgsToDictionary(args);
                foreach (var item in argsPairs)
                {
                    switch (item.Key)
                    {
                        case "-c":
                            if (data.TaskStarted) Console.WriteLine($"Задача {data.Description} " +
                                $"запущена {data.DateStart}");
                            break;
                        case "-d":
                            if (item.Value == string.Empty)
                            {
                                Console.WriteLine("Необходимо ввести параметр");
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
                            Console.WriteLine($"Неизвестный аргумент: {item.Key}");
                            return;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Произошла ошибка");
            }
        }
    }

    public class SettingCommand : ToolsForCommands, ICommand
    {
        public string Name => "settings";

        public string Description => "\n" +
            "Структура: settings [Аргумент] \n" +
            "Отвечает за настройку программы\n" +
            "Аргументы: \n" +
            "-u [Параметр]: смена пользовательского имени";

        public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
        {
            try
            {
                await Task.CompletedTask;
                if (args.Length == 0)
                {
                    Console.WriteLine("Необходимо ввести аргументы");
                    return;
                }
                Dictionary<string, string> argsPairs = ConvertArgsToDictionary(args);
                foreach (var item in argsPairs)
                {
                    switch (item.Key)
                    {
                        case "-u":
                            if(item.Value is null)
                            {
                                Console.WriteLine("Необходимо ввести параметр");
                                return;
                            }
                            data.Config.Username = item.Value!;
                            data.Config.CreateConfig();
                            break;
                        default:
                            Console.WriteLine($"Неизвестный аргумент: {item.Key}");
                            break;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Произошла ошибка");
            }
            throw new NotImplementedException();
        }
    }

    public class TargetsCommand : ToolsForCommands, ICommand
    {
        public string Name => "targets";

        public string Description => "\n" +
            "Структура: targets [Аргумент] \n" +
            "Отвечает за управление задачами\n" +
            "Аргументы: \n" +
            "-d [Параметр]: указывает на дату для поиска задач\n" +
            "-t [Параметр]: указывает на сегодняшнюю дату\n" +
            "-y [Параметр]: указывает на вчерашнюю дату\n";

        public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
        {
            try
            {
                await Task.CompletedTask;
                if (args.Length == 0)
                {
                    Console.WriteLine("Необходимо ввести аргументы");
                    return;
                }
                Dictionary<string, string> argsPairs = ConvertArgsToDictionary(args);
                var targetsService = provider.GetService<ITargetsService>();
                List<Targets> result = new List<Targets>();
                DateOnly date;
                foreach (var item in argsPairs)
                {
                    switch (item.Key)
                    {
                        case "-d":
                            if (item.Value == string.Empty)
                            {
                                Console.WriteLine($"Необходимо ввести параметр для {item.Value} ");
                                return;
                            }
                            date = DateOnly.Parse(item.Value);
                            result = await targetsService!.GetByDateAsync(date);
                            foreach (var target in result)
                            {
                                Console.WriteLine(target.ToString());
                            }
                            break;
                        case "-t":
                            date = DateOnly.FromDateTime(DateTime.Now);
                            result = await targetsService!.GetByDateAsync(date);
                            foreach (var target in result)
                            {
                                Console.WriteLine(target.ToString());
                            }
                            break;
                        case "-y":
                            date = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));
                            result = await targetsService!.GetByDateAsync(date);
                            foreach (var target in result)
                            {
                                Console.WriteLine(target.ToString());
                            }
                            break;
                        default:
                            Console.WriteLine($"Неизвестный аргумент: {item.Key}");
                            return;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Произошла ошибка : {ex.Message}");
            }
        }
    }

    public class TargetsAddCommand : ToolsForCommands, ICommand
    {
        public string Name => "targets-add";

        public string Description => "\n" +
            "Структура: targets-add [Аргументы] \n" +
            "Отвечает за добавление задач\n" +
            "Аргументы: \n" +
            "-n [Параметр]: указывает на название задачи\n" +
            "-d [Параметр]: указывает на дату задачи\n" +
            "-m [Параметр]: указывает на продолжительность задачи \n";

        public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
        {
            try
            {
                await Task.CompletedTask;
                if (args.Length == 0)
                {
                    Console.WriteLine("Необходимо ввсети аргументы");
                    return;
                }
                var argsPairs = ConvertArgsToDictionary(args);
                string description = string.Empty;
                DateOnly date = DateOnly.MinValue;
                int durationMinute = 0;
                foreach (var item in argsPairs)
                {
                    switch (item.Key)
                    {
                        case "-n":
                            description = item.Value;
                            break;
                        case "-d":
                            date = DateOnly.Parse(item.Value);
                            break;
                        case "-m":
                            durationMinute = Convert.ToInt32(item.Value);
                            break;
                        default:
                            Console.WriteLine($"Неизвестный аргумент {item.Key}");
                            break;
                    }
                }
                if (description == string.Empty)
                {
                    Console.WriteLine("Нулевой параметр -n, воспользуйтесь командой:" +
                        " ? targets-add для получения помощи");
                    return;
                }
                if (date == DateOnly.MinValue)
                {
                    Console.WriteLine("Нулевой параметр -d, воспользуйтесь командой:" +
                        " ? targets-add для получения помощи");
                    return;
                }
                if (durationMinute == 0)
                {
                    Console.WriteLine("Нулевой параметр -m, воспользуйтесь командой:" +
                        " ? targets-add для получения помощи");
                    return;
                }
                var targetService = provider.GetService<ITargetsService>();
                if (await targetService!.IsHaveAsync(description, date))
                {
                    Console.WriteLine("Данная цель уже существует");
                }
                else
                {
                    var target = Targets.Create(date, description, durationMinute);
                    if (!string.IsNullOrEmpty(target.error))
                    {
                        Console.WriteLine(target.error);
                        return;
                    }
                    await targetService.AddAsync(target.target!);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка : {ex.Message}");
            }
        }
    }

    public class TargetsDeleteCommand : ToolsForCommands, ICommand
    {
        public string Name => "targets-del";

        public string Description => "\n" +
            "Структура: target-del [Аргумент] \n" +
            "Отвечает за управление задачами\n" +
            "Аргументы: \n" +
            "-d [Параметр]: указывает на дату задачи\n" +
            "-n [Параметр]: указывает на название задачи\n";

        public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
        {
            try
            {
                await Task.CompletedTask;
                if (args.Length == 0)
                {
                    Console.WriteLine("Необходимо ввести аргументы");
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
                            date = DateOnly.Parse(item.Value);
                            break;
                        case "-n":
                            description = item.Value;
                            break;
                        default:
                            Console.WriteLine($"Неизввестный аргумент : {item.Key}");
                            return;
                    }
                }
                if (description == string.Empty)
                {
                    Console.WriteLine("Нулевой параметр -n, воспользуйтесь командой:" +
                        " ? targets-add для получения помощи");
                    return;
                }
                if (date == DateOnly.MinValue)
                {
                    Console.WriteLine("Нулевой параметр -d, воспользуйтесь командой:" +
                         " ? targets-add для получения помощи");
                    return;
                }
                var targetService = provider.GetService<ITargetsService>();
                if (!await targetService!.IsHaveAsync(description, date))
                {
                    Console.WriteLine("Объект не найден");
                }
                else
                {
                    await targetService.DeleteByDescriptionAsync(description);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка : {ex.Message}");
            }
        }
    }
}
