using ParkingApp.Core.Entities;
using ParkingApp.Core.Interfaces;

namespace ParkingApp.Infrastructure.Memory;

public class MemoryParkingSessionRepository : GenericRepositoryAsync<ParkingSession>, IParkingSessionRepository
{
    public Task<IEnumerable<ParkingSession>> FindByLicensePlateAsync(string licensePlate)
    {
        if (string.IsNullOrWhiteSpace(licensePlate))
        {
            throw new ArgumentException("License plate cannot be empty.", nameof(licensePlate));
        }

        var sessions = _data.Values
            .Where(s => s.Vehicle is not null &&
                        s.Vehicle.LicensePlate.Equals(licensePlate, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Task.FromResult<IEnumerable<ParkingSession>>(sessions);
    }

    public Task<IEnumerable<ParkingSession>> FindActiveSessionsAsync()
    {
        var sessions = _data.Values
            .Where(s => s.IsActive)
            .ToList();

        return Task.FromResult<IEnumerable<ParkingSession>>(sessions);
    }

    public Task<IEnumerable<ParkingSession>> FindHistoryByLicensePlateAsync(string licensePlate)
    {
        if (string.IsNullOrWhiteSpace(licensePlate))
        {
            throw new ArgumentException("License plate cannot be empty.", nameof(licensePlate));
        }

        var sessions = _data.Values
            .Where(s => s.Vehicle is not null &&
                        s.Vehicle.LicensePlate.Equals(licensePlate, StringComparison.OrdinalIgnoreCase) &&
                        !s.IsActive)
            .ToList();

        return Task.FromResult<IEnumerable<ParkingSession>>(sessions);
    }
}
