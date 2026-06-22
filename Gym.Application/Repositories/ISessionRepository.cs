using Gym.Domain.Entities;

namespace Gym.Application.Repositories;

public interface ISessionRepository
{
    Task<IReadOnlyList<Session>> GetAllWithBookingsAsync(CancellationToken ct = default);
    Task<Session?> GetByIdWithBookingsAsync(int id, CancellationToken ct = default);
    Task AddAsync(Session session, CancellationToken ct = default);
    void Update(Session session);
    void Delete(Session session);
}
