using ParkingApp.Core.Common;
using ParkingApp.Core.DTOs.ParkingGates;
using ParkingApp.Core.Entities;
using ParkingApp.Core.Interfaces;
using ParkingApp.Core.Interfaces.Services;

namespace ParkingApp.Infrastructure.Memory;

public class MemoryParkingGateService(IParkingUnitOfWork unit) : IParkingGateService
{
    public async Task<PagedResult<ParkingGateDto>> GetAllAsync(int page, int pageSize)
    {
        var paged = await unit.Gates.FindPagedAsync(page, pageSize);
        var items = paged.Items.Select(ToDto).ToList();
        return new PagedResult<ParkingGateDto>(items, paged.TotalCount, paged.Page, paged.PageSize);
    }

    public async Task<ParkingGateDto?> GetByIdAsync(Guid id)
    {
        var entity = await unit.Gates.FindByIdAsync(id);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<ParkingGateDto?> GetByNameAsync(string name)
    {
        var entity = await unit.Gates.FindByNameAsync(name);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<ParkingGateDto> CreateAsync(CreateGateDto dto)
    {
        var entity = dto.ToEntity();
        var added = await unit.Gates.AddAsync(entity);
        await unit.SaveChangesAsync();
        return ToDto(added);
    }

    public async Task<ParkingGateDto> SetOperationalStatusAsync(Guid id, bool isOperational)
    {
        var entity = await unit.Gates.FindByIdAsync(id);
        if (entity is null)
            throw new KeyNotFoundException($"Nie znaleziono bramki o id {id}");

        entity.IsOperational = isOperational;
        var updated = await unit.Gates.UpdateAsync(entity);
        await unit.SaveChangesAsync();
        return ToDto(updated);
    }

    private static ParkingGateDto ToDto(ParkingGate entity) =>
        new(
            entity.Id,
            entity.Name,
            entity.Type.ToString(),
            entity.Location,
            entity.IsOperational
        );
}
