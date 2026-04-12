using ParkingApp.Core.Entities;
using ParkingApp.Core.Enums;
using ParkingApp.Core.Interfaces;

namespace ParkingApp.Infrastructure.Memory;

public class MemoryParkingGateRepository : GenericRepositoryAsync<ParkingGate>, IParkingGateRepository
{
    public MemoryParkingGateRepository()
    {
        var gate1 = new ParkingGate()
        {
            Id = Guid.NewGuid(),
            Name = "Entry Gate",
            Type = GateType.Entry,
            Location = "Main Entrance",
            IsOperational = true
        };
        _data.Add(gate1.Id, gate1);

        var gate2 = new ParkingGate()
        {
            Id = Guid.NewGuid(),
            Name = "Exit Gate",
            Type = GateType.Exit,
            Location = "Main Exit",
            IsOperational = true
        };
        _data.Add(gate2.Id, gate2);
    }

    public Task<ParkingGate?> FindByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("nazwa musi istnieć", nameof(name));
        }

        var gate = _data.Values
            .FirstOrDefault(g => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(gate);
    }
}
