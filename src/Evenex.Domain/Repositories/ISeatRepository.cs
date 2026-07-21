using Evenex.Domain.Entities;

namespace Evenex.Domain.Repositories;

public interface ISeatRepository
{
    Task<Seat?> GetByIdAsync(int seatId);
}
