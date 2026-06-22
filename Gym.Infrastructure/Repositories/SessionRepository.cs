using Gym.Application.Repositories;
using Gym.Domain.Entities;
using Gym.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Gym.Infrastructure.Repositories;

public class SessionRepository(GymDbContext dbContext) : ISessionRepository
{
    public async Task<IReadOnlyList<Session>> GetAllWithBookingsAsync(CancellationToken ct = default)
        => await dbContext.Sessions
            .Include(s => s.Bookings.Where(b => !b.IsDeleted))
            .OrderBy(s => s.StartDate)
            .ToListAsync(ct);

    public async Task<Session?> GetByIdWithBookingsAsync(int id, CancellationToken ct = default)
        => await dbContext.Sessions
            .Include(s => s.Bookings.Where(b => !b.IsDeleted))
            .FirstOrDefaultAsync(s => s.Id == id, ct);

    public async Task AddAsync(Session session, CancellationToken ct = default)
        => await dbContext.Sessions.AddAsync(session, ct);

    public void Update(Session session)
        => dbContext.Sessions.Update(session);

    public void Delete(Session session)
        => dbContext.Sessions.Remove(session);
}
