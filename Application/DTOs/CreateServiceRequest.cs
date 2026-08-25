using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class CreateServiceRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int DurationInMinutes { get; set; }
        public bool IsActive { get; set; } = true;
    }
}