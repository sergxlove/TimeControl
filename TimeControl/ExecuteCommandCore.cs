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
}
