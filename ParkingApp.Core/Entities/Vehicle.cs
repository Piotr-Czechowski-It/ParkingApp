using ParkingApp.Core.Common;
using ParkingApp.Core.Entities;

namespace ParkingApp.Core.Entities;

public class Vehicle : EntityBase
{   
    public string LicensePlate { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;

    
    public ICollection<ParkingSession> ParkingSessions { get; set; } = new List<ParkingSession>();
}