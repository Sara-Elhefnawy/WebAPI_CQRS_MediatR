using Gym.Application.UOW;
using Gym.Domain.Common;
using MediatR;

namespace Gym.Application.Features.Sessions.Queries.GetSessions;

public sealed class GetSessionsQueryHandler(IUnitOfWork uow) : IRequestHandler<GetSessionsQueryRequest, Result<List<SessionListResponse>>>
{
    public async Task<Result<List<SessionListResponse>>> Handle(
        GetSessionsQueryRequest request,
        CancellationToken ct)
    {
        var sessions = await uow.Sessions.GetAllWithBookingsAsync(ct);

        var responses = sessions.Select(s => new SessionListResponse(
            Id: s.Id,
            CategoryName: s.CategoryName,
            TrainerName: s.TrainerName,
            Description: s.Description,
            StartDate: s.StartDate,
            EndDate: s.EndDate,
            Capacity: s.Capacity,
            AvailableSlots: s.AvailableSlots,
            Status: s.Status
        )).ToList();

        return Result.Ok(responses);
    }
}
