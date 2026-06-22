using Gym.Domain.Common;
using MediatR;

namespace Gym.Application.Features.Bookings.Commands.CreateBooking;

public record CreateBookingCommandRequest(
    int SessionId,
    int MemberId
) : IRequest<Result<int>>;
