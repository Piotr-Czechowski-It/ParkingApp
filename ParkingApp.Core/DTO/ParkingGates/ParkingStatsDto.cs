namespace ParkingApp.Core.DTOs.Stats;

public record ParkingStatsDto(
    int ActiveVehicles,
    decimal TodayRevenue,
    int TodayEntries,
    int TodayExits
);