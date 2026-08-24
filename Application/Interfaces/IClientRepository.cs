using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        Task<Client?> GetByNameAsync(string name);
    }
}
