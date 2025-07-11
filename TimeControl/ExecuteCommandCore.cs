using TimeControl.Interfaces;

namespace TimeControl
{
    public class ExecuteCommandCore
    {
        public List<ICommand> Commands { get; } = [];

        public void ExecuteCommand(string command, DataCore data)
        {
            string[] parts = command.Split(' ');
            string cmdName = parts[0];
            string[] args = parts.Skip(1).ToArray();
            var cmd = Commands.FirstOrDefault(a => a.Name == cmdName);
            if (cmd != null)
            {
                cmd.Execute(args, data);
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
