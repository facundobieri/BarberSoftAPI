using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappings
{
    public static class ServiceMapping
    {
        public static ServiceDto MapToDto(this Service s) => new ServiceDto
        {
            Id = s.Id,
            Name = s.Name,
            Price = s.Price,
            DurationInMinutes = s.DurationInMinutes,
            IsActive = s.IsActive
        };

        public static Service MapToEntity(this CreateServiceRequest r) => new Service
        {
            Name = r.Name,
            Price = r.Price,
            DurationInMinutes = r.DurationInMinutes,
            IsActive = r.IsActive
        };

        public static void UpdateFromRequest(this UpdateServiceRequest r, Service s)
        {
            if (!string.IsNullOrWhiteSpace(r.Name)) s.Name = r.Name;
            if (r.Price.HasValue) s.Price = r.Price.Value;
            if (r.DurationInMinutes.HasValue) s.DurationInMinutes = r.DurationInMinutes.Value;
            if (r.IsActive.HasValue) s.IsActive = r.IsActive.Value;
        }
    }
}
