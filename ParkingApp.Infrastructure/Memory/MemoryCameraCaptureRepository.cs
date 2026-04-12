using ParkingApp.Core.Entities;
using ParkingApp.Core.Interfaces;

namespace ParkingApp.Infrastructure.Memory;

public class MemoryCameraCaptureRepository : GenericRepositoryAsync<CameraCapture>, ICameraCaptureRepository
{
    public Task<IEnumerable<CameraCapture>> FindByLicensePlateAsync(string licensePlate)
    {
        if (string.IsNullOrWhiteSpace(licensePlate))
            throw new ArgumentException("rejestracja nie może być pusta", nameof(licensePlate));

        IEnumerable<CameraCapture> captures = _data.Values
            .Where(c => c.LicensePlate.Equals(licensePlate, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Task.FromResult(captures);
    }

    public Task<IEnumerable<CameraCapture>> FindByGateNameAsync(string gateName)
    {
        if (string.IsNullOrWhiteSpace(gateName))
            throw new ArgumentException("nazwa bramy nie może być pusta", nameof(gateName));

        IEnumerable<CameraCapture> captures = _data.Values
            .Where(c => c.GateName.Equals(gateName, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Task.FromResult(captures);
    }
}
