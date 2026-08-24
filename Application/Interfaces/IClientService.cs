using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GetAllAsync();
        Task<ClientDto?> GetByNameAsync(string name);
        Task<ClientDto> CreateClientAsync(CreateClientRequest request);
        Task<ClientDto?> UpdateClientAsync(int id, UpdateClientRequest request);
        Task<bool> DeleteClientAsync(int id);
    }
}
