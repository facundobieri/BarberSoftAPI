using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceDto>> GetAllAsync();
        Task<ServiceDto?> GetByNameAsync(string name);
        Task<ServiceDto> CreateAsync(CreateServiceRequest request);
        Task<ServiceDto?> UpdateAsync(int id, UpdateServiceRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
