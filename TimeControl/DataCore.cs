namespace TimeControl
{
    public class DataCore
    {
        public DateTime DateStart { get; set; }

        public string Description { get; set; } = string.Empty;

        public bool TaskStarted = false;

        public void Clear()
        {
            Description = string.Empty;
            TaskStarted = false;
        }
    }
}
