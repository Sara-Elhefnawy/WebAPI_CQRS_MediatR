using System.ComponentModel.DataAnnotations;

namespace Gym.API.ViewModels.Sessions;

public class CreateSessionViewModel
{
    [Required]
    [MaxLength(100)]
    public string CategoryName { get; set; } = default!;

    [Required]
    [MaxLength(100)]
    public string TrainerName { get; set; } = default!;

    [MaxLength(500)]
    public string Description { get; set; } = default!;

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Range(1, 25)]
    public int Capacity { get; set; }
}
