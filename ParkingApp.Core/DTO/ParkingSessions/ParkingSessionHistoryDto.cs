using ParkingApp.Core.DTOs.Vehicles;

namespace ParkingApp.Core.DTOs.ParkingSessions;

public record ParkingSessionHistoryDto(
    Guid SessionId,
    VehicleDto Vehicle,
    string GateName,
    DateTime EntryTime,
    DateTime? ExitTime,
    TimeSpan? Duration,
    decimal? Fee,
    bool IsActive
);