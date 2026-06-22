using Gym.Application.UOW;
using Gym.Domain.Common;
using MediatR;

namespace Gym.Application.Features.Sessions.Queries.GetSessionById;

public sealed class GetSessionByIdQueryHandler(IUnitOfWork uow) : IRequestHandler<GetSessionByIdQueryRequest, Result<SessionDetailResponse>>
{
    public async Task<Result<SessionDetailResponse>> Handle(
        GetSessionByIdQueryRequest request,
        CancellationToken ct)
    {
        var session = await uow.Sessions.GetByIdWithBookingsAsync(request.Id, ct);

        if (session is null)
            return Result.Fail<SessionDetailResponse>("Session not found.", "SESSION_NOT_FOUND");

        var response = new SessionDetailResponse(
            Id: session.Id,
            CategoryName: session.CategoryName,
            TrainerName: session.TrainerName,
            Description: session.Description,
            StartDate: session.StartDate,
            EndDate: session.EndDate,
            Capacity: session.Capacity,
            AvailableSlots: session.AvailableSlots,
            Status: session.Status
        );

        return Result.Ok(response);
    }
}
