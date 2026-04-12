using ParkingApp.Core.Entities;
using ParkingApp.Core.Interfaces;

namespace ParkingApp.Infrastructure.Memory;

public class MemoryParkingTariffRepository : GenericRepositoryAsync<ParkingTariff>, IParkingTariffRepository
{
    public Task<ParkingTariff?> FindByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("nazwa nie może być pusta", nameof(name));

        var tariff = _data.Values
            .FirstOrDefault(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(tariff);
    }

    public Task<IEnumerable<ParkingTariff>> FindActiveAsync()
    {
        IEnumerable<ParkingTariff> tariffs = _data.Values
            .Where(t => t.IsActive)
            .ToList();

        return Task.FromResult(tariffs);
    }
}
