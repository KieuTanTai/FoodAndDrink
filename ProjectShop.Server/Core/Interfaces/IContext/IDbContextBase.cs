using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace ProjectShop.Server.Core.Interfaces.IContext;

public interface IDbContextBase : IDisposable
{
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    IDbContextTransaction BeginTransaction();
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel);
    Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default);

    void CommitTransaction();
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    void RollbackTransaction();
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
