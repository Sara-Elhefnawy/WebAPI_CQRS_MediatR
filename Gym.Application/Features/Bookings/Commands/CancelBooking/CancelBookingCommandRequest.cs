using Gym.Domain.Common;
using MediatR;

namespace Gym.Application.Features.Bookings.Commands.CancelBooking;

public record CancelBookingCommandRequest(int BookingId) : IRequest<Result>;
