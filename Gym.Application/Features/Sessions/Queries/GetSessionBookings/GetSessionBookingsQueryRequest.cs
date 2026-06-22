using Gym.Domain.Common;
using MediatR;

namespace Gym.Application.Features.Sessions.Queries.GetSessionBookings;

public record GetSessionBookingsQueryRequest(int SessionId) : IRequest<Result<List<BookingListResponse>>>;

public record BookingListResponse(
    int Id,
    int MemberId,
    DateTime BookingDate,
    bool IsAttended
);
