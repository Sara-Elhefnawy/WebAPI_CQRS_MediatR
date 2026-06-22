using Gym.Domain.Common;
using MediatR;

namespace Gym.Application.Features.Sessions.Commands.UpdateSession;

public record UpdateSessionCommandRequest(
    int Id,
    string TrainerName,
    string Description,
    DateTime StartDate,
    DateTime EndDate
) : IRequest<Result>;
