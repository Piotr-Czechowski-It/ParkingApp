using ParkingApp.Core.Entities;
using ParkingApp.Core.Interfaces;

namespace ParkingApp.Infrastructure.Memory;

public class ParkingGateRepository : GenericRepositoryAsync<ParkingGate>, IParkingGateRepository
{
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