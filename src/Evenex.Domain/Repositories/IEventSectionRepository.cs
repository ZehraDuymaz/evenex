namespace Evenex.Domain.Repositories;

public interface IEventSectionRepository
{
    Task<Evenex.Domain.Entities.EventSection?> GetByIdAsync(int id);
    Task IncrementCapacityAsync(int eventSectionId);
}
