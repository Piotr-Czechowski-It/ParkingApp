using Microsoft.AspNetCore.Mvc;
using ParkingApp.Core.DTOs.ParkingGates;
using ParkingApp.Core.Interfaces.Services;

namespace ParkingApp.WebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class GatesController(IParkingGateService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllGates([FromQuery] int page, [FromQuery] int size)
    {
        return Ok(await service.GetAllAsync(page, size));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetGate(Guid id)
    {
        var gate = await service.GetByIdAsync(id);
        if (gate is null)
            return NotFound();

        return Ok(gate);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGate([FromBody] CreateGateDto dto)
    {
        var created = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetGate), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateGate(Guid id, [FromBody] UpdateGateDto dto)
    {
        var updated = await service.UpdateAsync(id, dto);
        if (updated is null)
            return NotFound();

        return Ok(updated);
    }
}
