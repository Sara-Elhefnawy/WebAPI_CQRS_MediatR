using Gym.Domain.Common;
using MediatR;

namespace Gym.Application.Features.Bookings.Queries.GetBookingById;

public record GetBookingByIdQueryRequest(int Id) : IRequest<Result<BookingDetailResponse>>;

public record BookingDetailResponse(
    int Id,
    int SessionId,
    int MemberId,
    DateTime BookingDate,
    bool IsAttended
);
