namespace ParkingApp.Core.DTOs.ParkingGates;

public record CreateGateDto(
    string Name,
    string Type,
    string Location
);