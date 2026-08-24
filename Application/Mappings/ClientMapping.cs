using System;
using System.Collections.Generic;
using System.Text;
using Application.DTOs;
using Domain.Entities;

namespace Application.Mappings
{
    public static class ClientMapping
    {
        public static ClientDto MapToDto(this Client client) => new ClientDto
        {
            Id = client.Id,
            Name = client.Name,
            Phone = client.Phone
        };

        public static Client MapToEntity(this CreateClientRequest request) => new Client
        {
            Name = request.Name,
            Phone = request.Phone
        };

        public static void UpdateFromRequest(this UpdateClientRequest request, Client client)
        {
            if (!string.IsNullOrWhiteSpace(request.Name)) client.Name = request.Name;
            if (!string.IsNullOrWhiteSpace(request.Phone)) client.Phone = request.Phone;
        }
    }
}