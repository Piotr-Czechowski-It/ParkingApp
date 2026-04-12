using ParkingApp.Core.Entities;

namespace ParkingApp.Core.Interfaces;

public interface ICameraCaptureRepository : IGenericRepositoryAsync<CameraCapture>
{
    Task<IEnumerable<CameraCapture>> FindByLicensePlateAsync(string licensePlate);
    Task<IEnumerable<CameraCapture>> FindByGateNameAsync(string gateName);
}
