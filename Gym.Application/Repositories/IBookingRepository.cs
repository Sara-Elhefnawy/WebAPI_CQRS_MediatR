using Gym.Domain.Entities;

namespace Gym.Application.Repositories;

public interface IBookingRepository
{
    Task<IReadOnlyList<Booking>> GetBySessionIdAsync(int sessionId, CancellationToken ct = default);
    Task<Booking?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<bool> ExistsAsync(int memberId, int sessionId, CancellationToken ct = default);
    Task AddAsync(Booking booking, CancellationToken ct = default);
}
