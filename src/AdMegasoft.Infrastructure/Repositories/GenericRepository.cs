﻿using AdMegasoft.Core.Repositories;
using AdMegasoft.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AdMegasoft.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        public GenericRepository(AdMegasoftDbContext context, DbSet<T> dbSet)
        {
            Context = context;
            DbSet = dbSet;
        }

        public AdMegasoftDbContext Context { get; }
        public DbSet<T> DbSet { get; }

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

        public async Task<List<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }
    }
}
