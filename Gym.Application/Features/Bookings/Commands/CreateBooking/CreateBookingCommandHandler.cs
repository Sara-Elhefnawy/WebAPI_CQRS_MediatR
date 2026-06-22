using Gym.Application.UOW;
using Gym.Domain.Common;
using Gym.Domain.Entities;
using MediatR;

namespace Gym.Application.Features.Bookings.Commands.CreateBooking;

public sealed class CreateBookingCommandHandler(IUnitOfWork uow)
        : IRequestHandler<CreateBookingCommandRequest, Result<int>>
{
    public async Task<Result<int>> Handle(
        CreateBookingCommandRequest request,
        CancellationToken ct)
    {
        if (request.SessionId <= 0)
            return Result.Fail<int>("Session ID is required.", "SESSION_ID_REQUIRED");

        if (request.MemberId <= 0)
            return Result.Fail<int>("Member ID is required.", "MEMBER_ID_REQUIRED");

        var session = await uow.Sessions.GetByIdWithBookingsAsync(request.SessionId, ct);

        if (session is null)
            return Result.Fail<int>("Session not found.", "SESSION_NOT_FOUND");

        if (session.Status == "Completed")
            return Result.Fail<int>("Cannot book a completed session.", "SESSION_COMPLETED");

        if (session.AvailableSlots <= 0)
            return Result.Fail<int>("Session is fully booked.", "SESSION_FULL");

        var alreadyBooked = await uow.Bookings.ExistsAsync(
            request.MemberId, request.SessionId, ct);

        if (alreadyBooked)
            return Result.Fail<int>("Member already has a booking for this session.", "ALREADY_BOOKED");

        var booking = Booking.Create(request.SessionId, request.MemberId);

        await uow.Bookings.AddAsync(booking, ct);
        await uow.SaveChangesAsync(ct);

        return Result.Ok(booking.Id);
    }
}
