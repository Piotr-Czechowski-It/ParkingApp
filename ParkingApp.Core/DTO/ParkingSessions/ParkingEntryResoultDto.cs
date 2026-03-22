using ParkingApp.Core.DTOs.Vehicles;

namespace ParkingApp.Core.DTOs.ParkingSessions;

public record ParkingEntryResultDto(
    Guid SessionId,
    VehicleDto Vehicle,
    string GateName,
    DateTime EntryTime,
    string Message
);