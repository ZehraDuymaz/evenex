namespace Evenex.Domain.Repositories;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}
