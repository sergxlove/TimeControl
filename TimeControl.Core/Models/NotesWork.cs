namespace TimeControl.Core.Models
{
    public class NotesWork
    {
        public const int MIN_LENGTH_DESCRIPTION = 5;
        public const int MAX_LENGTH_DESCRIPTION = 200;
        public Guid Id { get; }

        public DateOnly DateWork { get; }

        public string Description { get; } = string.Empty;

        public int DurationHour { get; }

        public int DurationMinute { get; }

        private NotesWork(Guid id, DateOnly dateWork, string description, int durationHour, 
            int durationMinute)
        {
            Id = id;
            DateWork = dateWork;
            Description = description;
            DurationHour = durationHour;
            DurationMinute = durationHour;
        }

        public static (NotesWork? notesWork, string error) Create (Guid id, DateOnly dateWork, 
            string description, int durationHour, int durationMinute)
        {
            NotesWork? result = null;
            string error = string.Empty;

            if (!string.IsNullOrEmpty(description))
            {
                error = "description is null";
                return (result, error);
            }

            if (description.Length < MIN_LENGTH_DESCRIPTION
                || description.Length > MAX_LENGTH_DESCRIPTION)
            {
                error = "description invalid";
                return (result, error);
            }

            result = new NotesWork(id, dateWork, description, durationHour, durationMinute);
            return (result, error);
        }

        public static (NotesWork? notesWork, string error) Create (DateTime dateStart,
            DateTime dateEnd, string description)
        {
            NotesWork? result = null;
            string error = string.Empty;

            if(!string.IsNullOrEmpty(description))
            {
                error = "description is null";
                return (result, error);
            }

            if(description.Length < MIN_LENGTH_DESCRIPTION 
                ||  description.Length > MAX_LENGTH_DESCRIPTION)
            {
                error = "description invalid";
                return (result, error);
            }

            DateOnly dateWork = new DateOnly(dateStart.Year, dateStart.Month, dateStart.Day);
            int durationMinutes = (dateEnd - dateStart).Minutes;
            int durationHour = durationMinutes / 60;
            durationMinutes -= 60 * durationHour;

            result = new NotesWork(Guid.NewGuid(), dateWork, description, durationHour,
                durationMinutes);

            return (result,  error);
        }


    }
}
