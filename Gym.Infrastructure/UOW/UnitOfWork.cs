using Gym.Application.Repositories;
using Gym.Application.UOW;
using Gym.Infrastructure.Data;
using Gym.Infrastructure.Repositories;

namespace Gym.Infrastructure.UOW;

public class UnitOfWork(GymDbContext dbContext) : IUnitOfWork
{
    private ISessionRepository? _sessions;
    private IBookingRepository? _bookings;

    public ISessionRepository Sessions
        => _sessions ??= new SessionRepository(dbContext);

    public IBookingRepository Bookings
        => _bookings ??= new BookingRepository(dbContext);

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        => await dbContext.SaveChangesAsync(ct);
}
