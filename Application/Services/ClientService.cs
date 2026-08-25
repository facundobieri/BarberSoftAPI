using Application.DTOs;
using Application.Interfaces;
using Application.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;

        public ClientService(IClientRepository repository) => _repository = repository;

        public async Task<IEnumerable<ClientDto>> GetAllAsync()
        {
            var clients = await _repository.GetAllAsync();
            var list = new List<ClientDto>();
            foreach (var c in clients) list.Add(c.MapToDto());
            return list;
        }

        public async Task<ClientDto?> GetByNameAsync(string name)
        {
            var client = await _repository.GetByNameAsync(name);
            if (client == null) return null;
            return client.MapToDto();
        }

        public async Task<ClientDto> CreateClientAsync(CreateClientRequest request)
        {
            var existing = await _repository.GetByNameAsync(request.Name);
            if (existing != null) throw new InvalidOperationException("Client name already exists.");

            var client = request.MapToEntity();
            await _repository.AddAsync(client);
            await _repository.SaveChangesAsync();
            return client.MapToDto();
        }

        public async Task<ClientDto?> UpdateClientAsync(int id, UpdateClientRequest request)
        {
            var client = await _repository.GetByIdAsync(id);
            if (client == null) return null;

            request.UpdateFromRequest(client);
            _repository.Update(client);
            await _repository.SaveChangesAsync();
            return client.MapToDto();
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            var client = await _repository.GetByIdAsync(id);
            if (client == null) return false;

            _repository.Delete(client);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
