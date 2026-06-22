namespace Gym.API.ViewModels.Bookings;

public class BookingViewModel
{
    public int Id { get; set; }
    public int SessionId { get; set; }
    public int MemberId { get; set; }
    public DateTime BookingDate { get; set; }
    public bool IsAttended { get; set; }
}
