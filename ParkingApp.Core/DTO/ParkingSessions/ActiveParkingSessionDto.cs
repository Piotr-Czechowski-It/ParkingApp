using ParkingApp.Core.DTOs.Vehicles;

namespace ParkingApp.Core.DTOs.ParkingSessions;

public record ActiveParkingSessionDto(
    Guid SessionId,
    VehicleDto Vehicle,
    string GateName,
    DateTime EntryTime,
    TimeSpan CurrentDuration
);