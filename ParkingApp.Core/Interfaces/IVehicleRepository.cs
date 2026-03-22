using ParkingApp.Core.Entities;

namespace ParkingApp.Core.Interfaces;

public interface IVehicleRepository : IGenericRepositoryAsync<Vehicle>
{
    Task<Vehicle?> FindByLicensePlateAsync(string licensePlate);
}