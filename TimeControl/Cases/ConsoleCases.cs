using TimeControl.Interfaces;

namespace TimeControl.Cases
{
    public class ConsoleCases
    {
        public static ICommand[] UseConsoleCases()
        {
            ICommand[] commands =
            {
                new StartTime(),
                new StopTime(),
            };
            return commands;
        }
    }

    public class StartTime : ICommand
    {
        public string Name => "start";

        public string Description => throw new NotImplementedException();

        public void Execute(string[] args, DataCore data)
        {
            data.DateNow = DateTime.Now;
        }
    }

    public class StopTime : ICommand
    {
        public string Name => "stop";

        public string Description => throw new NotImplementedException();

        public void Execute(string[] args, DataCore data)
        {
            Console.WriteLine((DateTime.Now - data.DateNow).Milliseconds);
        }
    }
}
