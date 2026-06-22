using Gym.Domain.Common;
using MediatR;

namespace Gym.Application.Features.Sessions.Commands.CreateSession;

public record CreateSessionCommandRequest(
    string CategoryName,
    string TrainerName,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    int Capacity
) : IRequest<Result<int>>;
