namespace TimeControl
{
    public class DataCore
    {
        public DataCore(AppConfig config)
        {
            Config = config;
        }
        public DateTime DateStart { get; set; }

        public string Description { get; set; } = string.Empty;

        public bool TaskStarted { get; set; } = false;

        public AppConfig Config { get; set; }

        public void Clear()
        {
            Description = string.Empty;
            TaskStarted = false;
        }
    }
}
