using Microsoft.Extensions.DependencyInjection;
using TimeControl.Interfaces;

namespace TimeControl
{
    public class ExecuteCommandCore
    {
        public List<ICommand> Commands { get; } = [];

        public void ExecuteCommand(string command, DataCore data, ServiceProvider provider)
        {
            string[] parts = command.Split(' ');
            string cmdName = parts[0];
            string[] args = parts.Skip(1).ToArray();
            var cmd = Commands.FirstOrDefault(a => a.Name == cmdName);
            if (cmd != null)
            {
                cmd.Execute(args, data, provider);
            }
            else
            {
                Console.WriteLine($"Не удалось найти команду: {cmdName}. Воспользуйтесь командой " +
                    $"help для получения помощи");
            }
        }

        public void Add(ICommand command)
        {
            Commands.Add(command);
        }

        public void AddRange(ICommand[] commands)
        {
            Commands.AddRange(commands);
        }
    }

    public class ToolsForCommands
    {
        protected Dictionary<string, string> ConvertArgsToDictionary(string[] args)
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
            return argsPairs;
        }
    }
}
