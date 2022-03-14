using Application.Interfaces.Repositories;
using Domain.Common;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RepositoryAsync<T, TId> : IRepositoryAsync<T, TId> where T : class, IEntity<TId>
    {
        private readonly MegaDbContext _context;

        public RepositoryAsync(MegaDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> Entities => _context.Set<T>();

        public IQueryable<T> FromSqlRaw(string sql, params object[] parameters)
        {
            return _context.Set<T>().FromSqlRaw(sql, parameters);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public async Task<IQueryable<T>> AddRangeAsync(IQueryable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public Task RemoveAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }

        public Task RemoveRangeAsync(IQueryable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }

        public async Task<List<T>> ToListAsync()
        {
            return await _context
                .Set<T>()
                .ToListAsync();
        }

        public async Task<T?> GetByIdAsync(TId id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

    }
}
