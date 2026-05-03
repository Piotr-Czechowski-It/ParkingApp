using ParkingApp.Core.Common;
using ParkingApp.Core.DTOs.ParkingGates;

namespace ParkingApp.Core.Interfaces.Services;

public interface IParkingGateService
{
    Task<PagedResult<ParkingGateDto>> GetAllAsync(int page, int pageSize);
    Task<ParkingGateDto?> GetByIdAsync(Guid id);
    Task<ParkingGateDto?> GetByNameAsync(string name);
    Task<ParkingGateDto> CreateAsync(CreateGateDto dto);
    Task<ParkingGateDto?> UpdateAsync(Guid id, UpdateGateDto dto);
    Task<ParkingGateDto> SetOperationalStatusAsync(Guid id, bool isOperational);
}
