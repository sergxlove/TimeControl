namespace TimeControl.DataAccess.Sqlite.Models
{
    public class NotesWorkEntity
    {
        public Guid Id { get; set; }

        public DateOnly DateWork { get; set; }

        public string Description { get; set; } = string.Empty;

        public int DurationHour { get; set; }

        public int DurationMinute { get; set; }
    }
}
