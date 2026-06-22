using Gym.Application.Repositories;

namespace Gym.Application.UOW;

public interface IUnitOfWork
{
    ISessionRepository Sessions { get; }
    IBookingRepository Bookings { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
