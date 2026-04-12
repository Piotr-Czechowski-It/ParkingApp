using ParkingApp.Core.Entities;

namespace ParkingApp.Core.Interfaces;

public interface IParkingTariffRepository : IGenericRepositoryAsync<ParkingTariff>
{
    Task<ParkingTariff?> FindByNameAsync(string name);
    Task<IEnumerable<ParkingTariff>> FindActiveAsync();
}
