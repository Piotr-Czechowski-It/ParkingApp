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
}
