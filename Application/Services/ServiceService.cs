using Application.DTOs;
using Application.Interfaces;
using Application.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _repository;
        public ServiceService(IServiceRepository repository) =>_repository = repository;
        public async Task<IEnumerable<ServiceDto>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            var list = new List<ServiceDto>();
            foreach (var i in items) list.Add(i.MapToDto());
            return list;
        }
        public async Task<ServiceDto?> GetByNameAsync(string name)
        {
            var service = await _repository.GetByNameAsync(name);
            return service == null ? null : service.MapToDto();
        }

        public async Task<ServiceDto> CreateAsync(CreateServiceRequest request)
        {
            var existing = await _repository.GetByNameAsync(request.Name);
            if (existing != null) throw new InvalidOperationException("Service with same name exists.");

            var service = request.MapToEntity();
            await _repository.AddAsync(service);
            await _repository.SaveChangesAsync();
            return service.MapToDto();
        }

        public async Task<ServiceDto?> UpdateAsync(int id, UpdateServiceRequest request)
        {
            var service = await _repository.GetByIdAsync(id);
            if (service == null) return null;

            request.UpdateFromRequest(service);
            _repository.Update(service);
            await _repository.SaveChangesAsync();
            return service.MapToDto();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var service = await _repository.GetByIdAsync(id);
            if (service == null) return false;
            _repository.Delete(service);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
