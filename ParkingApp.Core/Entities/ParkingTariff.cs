using ParkingApp.Core.Common;

namespace ParkingApp.Core.Entities;

public class ParkingTariff : EntityBase
{ 
    public string Name { get; set; } = string.Empty;
    public TimeSpan FreeParkingDuration { get; set; }
    public decimal HourlyRate { get; set; }
    public decimal DailyMaxRate { get; set; }
    public bool IsActive { get; set; }

    public ICollection<ParkingSession> ParkingSessions { get; set; } = new List<ParkingSession>();
}