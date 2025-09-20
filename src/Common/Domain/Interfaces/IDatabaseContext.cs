using Microsoft.EntityFrameworkCore;

namespace Common.Domain.Interfaces
{
    public interface IDatabaseContext
    {
        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
