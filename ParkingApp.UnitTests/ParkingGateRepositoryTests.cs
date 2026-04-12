using ParkingApp.Core.Entities;
using ParkingApp.Core.Enums;
using ParkingApp.Core.Interfaces;
using ParkingApp.Infrastructure.Memory;

namespace ParkingApp.UnitTests.Repositories;

public class ParkingGateRepositoryTests
{
    private readonly IParkingGateRepository _repo = new MemoryParkingGateRepository();

    [Fact]
    public async Task FindByNameAsync_ShouldReturnGate_WhenNameExists()
    {
        var gate = new ParkingGate
        {
            Name = "Gate A",
            Type = GateType.Entry,
            Location = "North",
            IsOperational = true
        };

        await _repo.AddAsync(gate);

        var result = await _repo.FindByNameAsync("gate a");

        Assert.NotNull(result);
        Assert.Equal(gate.Id, result!.Id);
    }

    [Fact]
    public async Task FindByNameAsync_ShouldReturnNull_WhenNameDoesNotExist()
    {
        var result = await _repo.FindByNameAsync("Missing Gate");

        Assert.Null(result);
    }

    [Fact]
    public async Task FindByNameAsync_ShouldThrowArgumentException_WhenNameIsEmpty()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _repo.FindByNameAsync(" "));
    }
}
