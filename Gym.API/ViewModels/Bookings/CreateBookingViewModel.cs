using System.ComponentModel.DataAnnotations;

namespace Gym.API.ViewModels.Bookings;

public class CreateBookingViewModel
{
    [Required]
    public int SessionId { get; set; }

    [Required]
    public int MemberId { get; set; }
}
