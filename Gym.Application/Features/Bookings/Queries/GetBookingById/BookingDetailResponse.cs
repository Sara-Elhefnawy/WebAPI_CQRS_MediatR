namespace Gym.Application.Features.Bookings.Queries.GetBookingById;

public record BookingDetailResponse(
    int Id,
    int SessionId,
    int MemberId,
    DateTime BookingDate,
    bool IsAttended
);
