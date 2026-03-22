using ParkingApp.Core.Entities;

namespace ParkingApp.Core.Interfaces;

public interface IParkingGateRepository : IGenericRepositoryAsync<ParkingGate>
{
    Task<ParkingGate?> FindByNameAsync(string name);
}