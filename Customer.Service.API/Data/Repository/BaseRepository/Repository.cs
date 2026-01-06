using Customer.Service.API.Data.Models;
using Customer.Service.API.Data.Repository.BaseRepository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Customer.Service.API.Data.Repository.BaseRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;
        public readonly DbSet<T> _dbSet;
        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = appDbContext.Set<T>();
        }



        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return entities;
        }

        public async Task<bool?> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity is null) return false;
            _appDbContext.Remove(entity);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;

        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            var entities = await _dbSet.ToListAsync(cancellationToken);
            return entities;
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _dbSet.FindAsync(new object[] { id }, cancellationToken);
            return entity;
        }

        public async Task<T?> UpdateAsync(Guid id, T entity, CancellationToken cancellationToken)
        {
            var existing = await _dbSet.FindAsync(new object[] { id }, cancellationToken);
            if (existing is null) return null;

            _appDbContext.Entry(existing).CurrentValues.SetValues(entity);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return existing;
        }
    }
}
