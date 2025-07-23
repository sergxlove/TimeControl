namespace TimeControl.Core.Models
{
    public class Targets
    {
        public const int MIN_LENGTH_DESCRIPTION = 3;
        public const int MAX_LENGTH_DESCRIPTION = 200;
        public Guid Id { get; }

        public DateOnly DateWork { get; }

        public string Description { get; } = string.Empty;

        public int DurationMinutes { get; }

        public int DoneDurationMinutes { get; private set; }

        public bool IsDone { get; private set; }

        private Targets(Guid id, DateOnly dateWork, string description, int durationMinute,
            int doneDurationMinutes, bool isDone)
        {
            Id = id;
            DateWork = dateWork;
            Description = description;
            DurationMinutes = durationMinute;
            DoneDurationMinutes = doneDurationMinutes;
            IsDone = isDone;
        }

        public static (Targets? target, string error) Create (DateOnly dateWork,
            string description, int durationMinutes)
        {
            return Create(Guid.NewGuid(), dateWork, description, durationMinutes, 0, false);
        }

        public static (Targets? target, string error) Create (Guid id,DateOnly dateWork,
            string description, int durationMinutes, int doneDurationMinutes, bool isDone)
        {
            Targets? target = null;
            string error = string.Empty;

            if (string.IsNullOrEmpty(description))
            {
                error = "description is null";
                return (target, error);
            }

            if (description.Length < MIN_LENGTH_DESCRIPTION
                || description.Length > MAX_LENGTH_DESCRIPTION)
            {
                error = "description invalid";
                return (target, error);
            }

            target = new Targets(id, dateWork, description, durationMinutes, doneDurationMinutes, isDone);
            return (target, error);
        }

        public void Update(int doneDuration)
        {

        }

        public override string ToString()
        {
            return $"{DateWork}   |   {Description}   |   {DoneDurationMinutes}/{DurationMinutes}m  |  {IsDone}";
        }
    }
}
