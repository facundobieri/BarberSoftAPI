using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BarberSoft.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        public ServicesController(IServiceService serviceService) => _serviceService = serviceService;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _serviceService.GetAllAsync());

        [HttpGet("byname/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var s = await _serviceService.GetByNameAsync(name);
            if (s == null) return NotFound();
            return Ok(s);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServiceRequest req)
        {
            var created = await _serviceService.CreateAsync(req);
            return CreatedAtAction(nameof(GetByName), new { name = created.Name }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateServiceRequest req)
        {
            var updated = await _serviceService.UpdateAsync(id, req);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _serviceService.DeleteAsync(id);
            if (!result) return NotFound(new { message = "Service not found." });
            return NoContent();
        }
    }
}