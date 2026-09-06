using Application.DTOs;
using Application.Interfaces;
using Application.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;

        public UserService(IUserRepository repository) => _repository = repository;

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return null;
            return user?.MapToDto();
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _repository.GetAllAsync();
            return users.Select(u => u.MapToDto());
        }

        public async Task<UserDto> CreateUserAsync(CreateUserRequest request)
        {
            var existingUsername = await _repository.GetByUsernameAsync(request.Name);
            if (existingUsername != null)
                throw new InvalidOperationException("Username already exists.");

            var existingEmail = await _repository.GetByEmailAsync(request.Email);
            if (existingEmail != null)
                throw new InvalidOperationException("Email already exists.");
            var user = request.MapToEntity();
            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();
            return user.MapToDto();
        }

        public async Task<UserDto?> UpdateUserAsync(int id, UpdateUserRequest request)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return null;

            if (!string.IsNullOrWhiteSpace(request.Email) && request.Email != user.Email)
            {
                var existingEmail = await _repository.GetByEmailAsync(request.Email);
                if (existingEmail != null)
                    throw new InvalidOperationException("Email already exists.");
            }

            request.UpdateFromRequest(user);
            _repository.Update(user);
            await _repository.SaveChangesAsync();
            return user.MapToDto();
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return false;

            _repository.Delete(user);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
