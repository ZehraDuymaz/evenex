using Evenex.Domain.Entities;

namespace Evenex.Domain.Repositories;

public interface IReservationRepository
{
    Task<Reservation?> GetByIdAsync(int id);
    Task AddAsync(Reservation reservation);
    Task<List<Reservation>> GetExpiredHeldAsync(DateTime asOf);
}
