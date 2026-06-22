namespace Gym.Domain.Entities;

public class Booking
{
    public int Id { get; private set; }
    public int SessionId { get; private set; }
    public int MemberId { get; private set; }
    public DateTime BookingDate { get; private set; }
    public bool IsAttended { get; private set; }
    public bool IsDeleted { get; private set; }

    // EF Core navigation
    public Session Session { get; private set; } = default!;

    private Booking() { }

    public static Booking Create(int sessionId, int memberId)
    {
        return new Booking
        {
            SessionId = sessionId,
            MemberId = memberId,
            BookingDate = DateTime.Now,
            IsAttended = false,
            IsDeleted = false
        };
    }

    public void Cancel()
    {
        if (IsDeleted)
            throw new InvalidOperationException("Booking is already cancelled.");

        IsDeleted = true;
    }

    public void MarkAttended()
    {
        if (IsDeleted)
            throw new InvalidOperationException("Cannot mark attendance on a cancelled booking.");

        IsAttended = true;
    }
}
