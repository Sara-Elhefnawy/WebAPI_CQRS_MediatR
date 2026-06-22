using System.ComponentModel.DataAnnotations;

namespace Gym.API.ViewModels.Sessions;

public class UpdateSessionViewModel
{
    [Required]
    [MaxLength(100)]
    public string TrainerName { get; set; } = default!;

    [MaxLength(500)]
    public string Description { get; set; } = default!;

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }
}
