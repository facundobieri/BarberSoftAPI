using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly BarberSoftDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(BarberSoftDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public virtual async Task<T?> GetByIdAsync(int id) =>
            await _dbSet.SingleOrDefaultAsync();

        public virtual async Task<IEnumerable<T>> GetAllAsync() =>
            await _dbSet.ToListAsync();

        public virtual async Task AddAsync(T entity) =>
            await _dbSet.AddAsync(entity);

        public virtual void Update(T entity) =>
            _dbSet.Update(entity);

        public virtual void Delete(T entity) =>
            _dbSet.Remove(entity);

        public virtual async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
