using ParkingApp.Core.Entities;
using ParkingApp.Core.Interfaces;

namespace ParkingApp.Infrastructure.Memory;

public class MemoryVehicleRepository : GenericRepositoryAsync<Vehicle>, IVehicleRepository
{
    public Task<Vehicle?> FindByLicensePlateAsync(string licensePlate)
    {
        if (string.IsNullOrWhiteSpace(licensePlate))
        {
            throw new ArgumentException("rejestracja nie może być pusta", nameof(licensePlate));
        }

        var vehicle = _data.Values
            .FirstOrDefault(v => v.LicensePlate.Equals(licensePlate, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(vehicle);
    }
}
