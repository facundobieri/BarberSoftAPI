using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(BarberSoftDbContext context) : base(context) { }

        public async Task<Client?> GetByNameAsync(string name) =>
            await _dbSet.FirstOrDefaultAsync(c => c.Name == name);
    }
}