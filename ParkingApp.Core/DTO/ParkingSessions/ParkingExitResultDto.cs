using ParkingApp.Core.DTOs.Vehicles;

namespace ParkingApp.Core.DTOs.ParkingSessions;

public record ParkingExitResultDto(
    Guid SessionId,
    VehicleDto Vehicle,
    string GateName,
    DateTime EntryTime,
    DateTime ExitTime,
    TimeSpan Duration,
    TimeSpan FreeParkingDuration,
    decimal Fee,
    bool WasCharged,
    string Message
);