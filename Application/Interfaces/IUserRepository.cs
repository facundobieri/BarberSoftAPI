using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByUsernameAsync(string name);
        Task<User?> GetByEmailAsync(string email);
    }
}