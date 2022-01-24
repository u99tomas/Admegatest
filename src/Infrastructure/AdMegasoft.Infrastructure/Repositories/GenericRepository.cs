﻿using AdMegasoft.Abstractions.Abstractions;
using AdMegasoft.Domain.Common;
using AdMegasoft.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AdMegasoft.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class, IEntity
    {
        public AdMegasoftDbContext Context { get; }
        public DbSet<T> DbSet { get; }

        public GenericRepository(AdMegasoftDbContext context, DbSet<T> dbSet)
        {
            Context = context;
            DbSet = dbSet;
        }

        public async Task AddAsync(T model)
        {
            await DbSet.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task AddOrUpdateAsync(T model)
        {
            DbSet.Attach(model);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T model)
        {
            DbSet.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T model)
        {
            DbSet.Update(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync() => await DbSet.ToListAsync();

        public async Task<T?> GetByIdAsync(int id) => await DbSet.FindAsync(id);

        public IQueryable<T> AsQueryable() => DbSet.AsQueryable();
    }
}