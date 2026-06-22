namespace Gym.Application.Features.Sessions.Commands.UpdateSession;

public class UpdateSessionRequestDto
{
    public string TrainerName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
