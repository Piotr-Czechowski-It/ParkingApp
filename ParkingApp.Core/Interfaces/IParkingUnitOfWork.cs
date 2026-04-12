namespace ParkingApp.Core.Interfaces;

public interface IParkingUnitOfWork
{
    IVehicleRepository Vehicles { get; }
    IParkingGateRepository Gates { get; }
    IParkingSessionRepository Sessions { get; }
    IParkingTariffRepository Tariffs { get; }
    ICameraCaptureRepository CameraCaptures { get; }

    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
