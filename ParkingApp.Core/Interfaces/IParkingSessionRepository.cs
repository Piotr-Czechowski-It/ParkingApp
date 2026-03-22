using ParkingApp.Core.Entities;

namespace ParkingApp.Core.Interfaces;

public interface IParkingSessionRepository : IGenericRepositoryAsync<ParkingSession>
{
    Task<IEnumerable<ParkingSession>> FindByLicensePlateAsync(string licensePlate);
    Task<IEnumerable<ParkingSession>> FindActiveSessionsAsync();
    Task<IEnumerable<ParkingSession>> FindHistoryByLicensePlateAsync(string licensePlate);
}