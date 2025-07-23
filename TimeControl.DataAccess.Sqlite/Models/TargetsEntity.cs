namespace TimeControl.DataAccess.Sqlite.Models
{
    public class TargetsEntity
    {
        public Guid Id { get; set; }

        public DateOnly DateWork { get; set; }

        public string Description { get; set; } = string.Empty;

        public int DurationMinutes { get; set; }

        public int DoneDurationMinutes { get; set; }

        public bool IsDone { get; set; }
    }
}
