namespace Gym.Domain.Entities;

public class Session
{
    public int Id { get; private set; }
    public string CategoryName { get; private set; } = default!;
    public string TrainerName { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public int Capacity { get; private set; }

    public ICollection<Booking> Bookings { get; private set; } = [];


    private Session() { }

    public static Session Create(
        string categoryName,
        string trainerName,
        string description,
        DateTime startDate,
        DateTime endDate,
        int capacity)
    {
        if (string.IsNullOrWhiteSpace(categoryName))
            throw new ArgumentException("Category name is required.", nameof(categoryName));

        if (string.IsNullOrWhiteSpace(trainerName))
            throw new ArgumentException("Trainer name is required.", nameof(trainerName));

        if (startDate >= endDate)
            throw new ArgumentException("End date must be after start date.");

        if (startDate <= DateTime.Now)
            throw new ArgumentException("Start date must be in the future.");

        if (capacity < 1 || capacity > 25)
            throw new ArgumentException("Capacity must be between 1 and 25.");

        return new Session
        {
            CategoryName = categoryName.Trim(),
            TrainerName = trainerName.Trim(),
            Description = description.Trim(),
            StartDate = startDate,
            EndDate = endDate,
            Capacity = capacity
        };
    }

    public void Update(string trainerName, string description, DateTime startDate, DateTime endDate)
    {
        if (StartDate <= DateTime.Now)
            throw new InvalidOperationException("Cannot edit a session that has already started.");

        if (startDate >= endDate)
            throw new ArgumentException("End date must be after start date.");

        if (startDate <= DateTime.Now)
            throw new ArgumentException("Start date must be in the future.");

        TrainerName = trainerName.Trim();
        Description = description.Trim();
        StartDate = startDate;
        EndDate = endDate;
    }

    public int AvailableSlots => Capacity - Bookings.Count(b => !b.IsDeleted);

    public string Status
    {
        get
        {
            var now = DateTime.Now;
            if (now < StartDate) return "Upcoming";
            if (now <= EndDate) return "Ongoing";
            return "Completed";
        }
    }
}
