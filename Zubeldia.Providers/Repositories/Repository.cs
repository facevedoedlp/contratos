namespace Zubeldia.Providers.Repositories
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using System.Threading;
    using Microsoft.EntityFrameworkCore;
    using Zubeldia.Domain.Entities.Base;
    using Zubeldia.Domain.Interfaces.Providers;
    using Zubeldia.Domain.Session;
    using Zubeldia.Providers;

    [ExcludeFromCodeCoverage]
    public class Repository<TEntity>(ZubeldiaDbContext context, ISessionAccessor sessionAccessor) : IRepository<TEntity>
        where TEntity : class
    {
        private readonly ZubeldiaDbContext context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly SessionData? sessionData = sessionAccessor.Data;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return (IUnitOfWork)context;
            }
        }

        public virtual IQueryable<TEntity> GetAll(bool asNoTracking = true)
        {
            if (asNoTracking)
            {
                return context.Set<TEntity>().AsNoTracking();
            }
            else
            {
                return context.Set<TEntity>().AsQueryable();
            }
        }

        public virtual IQueryable<TEntity> GetAllBySpec(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true)
        {
            if (asNoTracking)
            {
                return context.Set<TEntity>().Where(predicate).AsNoTracking();
            }
            else
            {
                return context.Set<TEntity>().Where(predicate).AsQueryable();
            }
        }

        public virtual async Task<TEntity?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default)
            where TId : notnull
        {
            return await context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        }

        public virtual async Task<TEntity?> GetBySpecAsync<TSpec>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await context.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public virtual async Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await context.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken);
        }

        public virtual async Task<ICollection<TEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await context.Set<TEntity>().ToListAsync(cancellationToken);
        }

        public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await context.Set<TEntity>().CountAsync(cancellationToken);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await context.Set<TEntity>().Where(predicate).CountAsync(cancellationToken);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await context.Set<TEntity>().AnyAsync(predicate, cancellationToken);
        }

        public virtual async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return await context.Set<TEntity>().AnyAsync(cancellationToken);
        }

        public virtual IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = GetAll();
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include(includeProperty);
            }

            return queryable;
        }

        public virtual TEntity Add(TEntity entity)
        {
            return context.Set<TEntity>().Add(entity).Entity;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            context.Set<TEntity>().Add(entity);

            await SaveChangesAsync(cancellationToken);

            return entity;
        }

        public virtual ICollection<TEntity> AddRange(ICollection<TEntity> entities)
        {
            context.Set<TEntity>().AddRange(entities);

            return entities;
        }

        public virtual async Task<int> AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await context.Set<TEntity>().AddRangeAsync(entities);

            return await SaveChangesAsync(cancellationToken);
        }

        public virtual void Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        public virtual async Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            context.Set<TEntity>().Remove(entity);

            return await SaveChangesAsync(cancellationToken);
        }

        public virtual void DeleteRange(ICollection<TEntity> entities)
        {
            context.Set<TEntity>().RemoveRange(entities);
        }

        public virtual async Task<int> DeleteRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
        {
            context.Set<TEntity>().RemoveRange(entities);

            return await SaveChangesAsync(cancellationToken);
        }

        public virtual void Update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            context.Set<TEntity>().Update(entity);
            await SaveChangesAsync(cancellationToken);
            return entity;
        }

        public virtual void ClearChanges()
        {
            context.ChangeTracker.Clear();
        }

        public virtual async Task<TEntity> SaveOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var isForUpdate = context.Entry(entity).State == EntityState.Modified;
            if (isForUpdate)
            {
                return await UpdateAsync(entity, cancellationToken);
            }
            else
            {
                return await AddAsync(entity, cancellationToken);
            }
        }

        private async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                if (entry.Entity is AuditableEntity auditable)
                {
                    if (entry.State == EntityState.Added)
                    {
                        auditable.CreatedDate = auditable.CreatedDate.HasValue ? auditable.CreatedDate : DateTime.UtcNow;
                        auditable.CreatedBy = sessionData?.User?.Email;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        auditable.LastModificationDate = DateTime.UtcNow;
                        auditable.LastModificationBy = sessionData?.User?.Email;
                    }
                }
            }

            return await context.SaveChangesAsync(cancellationToken);
        }
    }
}
