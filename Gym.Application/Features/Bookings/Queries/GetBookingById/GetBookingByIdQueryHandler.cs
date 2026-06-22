using Gym.Application.UOW;
using Gym.Domain.Common;
using MediatR;

namespace Gym.Application.Features.Bookings.Queries.GetBookingById;

public sealed class GetBookingByIdQueryHandler(IUnitOfWork uow) : IRequestHandler<GetBookingByIdQueryRequest, Result<BookingDetailResponse>>
{
    public async Task<Result<BookingDetailResponse>> Handle(
        GetBookingByIdQueryRequest request,
        CancellationToken ct)
    {
        var booking = await uow.Bookings.GetByIdAsync(request.Id, ct);

        if (booking is null)
            return Result.Fail<BookingDetailResponse>("Booking not found.", "BOOKING_NOT_FOUND");

        var response = new BookingDetailResponse(
            Id: booking.Id,
            SessionId: booking.SessionId,
            MemberId: booking.MemberId,
            BookingDate: booking.BookingDate,
            IsAttended: booking.IsAttended
        );

        return Result.Ok(response);
    }
}
