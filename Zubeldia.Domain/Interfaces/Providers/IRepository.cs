﻿namespace Zubeldia.Domain.Interfaces.Providers
{
    using System.Threading;

    public interface IRepository<TEntity> : IReadRepository<TEntity>
        where TEntity : class
    {
        IUnitOfWork UnitOfWork { get; }
        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        ICollection<TEntity> AddRange(ICollection<TEntity> entities);
        Task<int> AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);
        void Delete(TEntity entity);
        Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        void DeleteRange(ICollection<TEntity> entities);
        Task<int> DeleteRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);
        void Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        void ClearChanges();
    }
}
