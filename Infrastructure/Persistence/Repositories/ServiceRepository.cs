using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class ServiceRepository : BaseRepository<Service>, IServiceRepository
    {
        public ServiceRepository(BarberSoftDbContext context) : base(context) { }

        public async Task<Service?> GetByNameAsync(string name) =>
            await _dbSet.FirstOrDefaultAsync(s => s.Name == name);
    }
}
