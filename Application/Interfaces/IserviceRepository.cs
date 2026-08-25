using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IServiceRepository : IBaseRepository<Service>
    {
        Task<Service?> GetByNameAsync(string name);
    }
}
