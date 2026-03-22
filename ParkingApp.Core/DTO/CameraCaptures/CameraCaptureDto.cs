namespace ParkingApp.Core.DTOs.CameraCaptures;

public record CameraCaptureDto(
    string LicensePlate,
    string Brand,
    string Color,
    string GateName,
    string? ImagePath = null
);