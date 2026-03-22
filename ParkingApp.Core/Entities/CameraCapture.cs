using ParkingApp.Core.Common;
using ParkingApp.Core.Enums;

namespace ParkingApp.Core.Entities;

public class CameraCapture : EntityBase
{
    public string GateName { get; set; } = string.Empty;
    public string LicensePlate { get; set; } = string.Empty;
    public string DetectedBrand { get; set; } = string.Empty;
    public string DetectedColor { get; set; } = string.Empty;
    public DateTime CapturedAt { get; set; }
    public string ImagePath { get; set; } = string.Empty;
    public CaptureType Type { get; set; }

    public Guid ParkingGateId { get; set; }
    public ParkingGate ParkingGate { get; set; } = null!;
}