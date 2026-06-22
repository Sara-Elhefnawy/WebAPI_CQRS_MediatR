using Gym.Application.UOW;
using Gym.Domain.Common;
using MediatR;

namespace Gym.Application.Features.Sessions.Queries.GetSessionBookings;

public sealed class GetSessionBookingsQueryHandler(IUnitOfWork uow)
        : IRequestHandler<GetSessionBookingsQueryRequest, Result<List<BookingListResponse>>>
{
    public async Task<Result<List<BookingListResponse>>> Handle(
        GetSessionBookingsQueryRequest request,
        CancellationToken ct)
    {
        var session = await uow.Sessions.GetByIdWithBookingsAsync(request.SessionId, ct);

        if (session is null)
            return Result.Fail<List<BookingListResponse>>("Session not found.", "SESSION_NOT_FOUND");

        var bookings = await uow.Bookings.GetBySessionIdAsync(request.SessionId, ct);

        var dtos = bookings
            .Where(b => !b.IsDeleted)
            .Select(b => new BookingListResponse(
                Id: b.Id,
                MemberId: b.MemberId,
                BookingDate: b.BookingDate,
                IsAttended: b.IsAttended))
            .ToList();

        return Result.Ok(dtos);
    }
}
