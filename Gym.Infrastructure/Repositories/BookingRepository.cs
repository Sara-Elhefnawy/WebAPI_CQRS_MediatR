using Gym.Application.Repositories;
using Gym.Domain.Entities;
using Gym.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Gym.Infrastructure.Repositories;

public class BookingRepository(GymDbContext dbContext) : IBookingRepository
{
    public async Task<IReadOnlyList<Booking>> GetBySessionIdAsync(int sessionId, CancellationToken ct = default)
        => await dbContext.Bookings
            .Where(b => b.SessionId == sessionId && !b.IsDeleted)
            .ToListAsync(ct);

    public async Task<Booking?> GetByIdAsync(int id, CancellationToken ct = default)
        => await dbContext.Bookings.FirstOrDefaultAsync(b => b.Id == id, ct);

    public async Task<bool> ExistsAsync(int memberId, int sessionId, CancellationToken ct = default)
        => await dbContext.Bookings.AnyAsync(
            b => b.MemberId == memberId && b.SessionId == sessionId && !b.IsDeleted, ct);

    public async Task AddAsync(Booking booking, CancellationToken ct = default)
        => await dbContext.Bookings.AddAsync(booking, ct);
}
