using Gym.Application.UOW;
using Gym.Domain.Common;
using MediatR;

namespace Gym.Application.Features.Bookings.Commands.CancelBooking;

public sealed class CancelBookingCommandHandler(IUnitOfWork uow) : IRequestHandler<CancelBookingCommandRequest, Result>
{
    public async Task<Result> Handle(CancelBookingCommandRequest request, CancellationToken ct)
    {
        var booking = await uow.Bookings.GetByIdAsync(request.BookingId, ct);

        if (booking is null)
            return Result.Fail("Booking not found.", "BOOKING_NOT_FOUND");

        try
        {
            booking.Cancel();
        }
        catch (InvalidOperationException ex)
        {
            return Result.Fail(ex.Message, "INVALID_CANCEL");
        }

        await uow.SaveChangesAsync(ct);
        return Result.Ok();
    }
}
