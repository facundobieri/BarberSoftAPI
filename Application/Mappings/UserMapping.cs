using Application.DTOs;
using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappings
{
    public static class UserMapping
    {
        public static UserDto MapToDto(this User user) => new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role.ToString()
        };

        public static User MapToEntity(this CreateUserRequest request) => new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            Role = Enum.TryParse<UserRole>(request.Role, out var role) ? role : UserRole.Barber
        };

        public static void UpdateFromRequest(this UpdateUserRequest request, User user)
        {
            if (!string.IsNullOrWhiteSpace(request.Name)) user.Name = request.Name;
            if (!string.IsNullOrWhiteSpace(request.Email)) user.Email = request.Email;
            if (!string.IsNullOrWhiteSpace(request.Password)) user.Password = request.Password;
            if (Enum.TryParse<UserRole>(request.Role, out var role)) user.Role = role;
        }
    }
}
