using ParkingApp.Core.Common;
using ParkingApp.Core.Enums;

namespace ParkingApp.Core.Entities;

public class ParkingGate : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public GateType Type { get; set; }
    public string Location { get; set; } = string.Empty;
    public bool IsOperational { get; set; }

    public ICollection<ParkingSession> ParkingSessions { get; set; } = new List<ParkingSession>();
    public ICollection<CameraCapture> CameraCaptures { get; set; } = new List<CameraCapture>();
}