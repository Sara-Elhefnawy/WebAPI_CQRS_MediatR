using Gym.Domain.Common;
using MediatR;

namespace Gym.Application.Features.Sessions.Queries.GetSessions;

public record GetSessionsQueryRequest : IRequest<Result<List<SessionListResponse>>>;

public record SessionListResponse(
    int Id,
    string CategoryName,
    string TrainerName,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    int Capacity,
    int AvailableSlots,
    string Status
);
