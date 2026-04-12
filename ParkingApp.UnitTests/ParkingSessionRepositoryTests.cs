using ParkingApp.Core.Entities;
using ParkingApp.Core.Enums;
using ParkingApp.Core.Interfaces;
using ParkingApp.Infrastructure.Memory;

namespace ParkingApp.UnitTests.Repositories;

public class ParkingSessionRepositoryTests
{
    private readonly IParkingSessionRepository _repo = new MemoryParkingSessionRepository();

    [Fact]
    public async Task FindByLicensePlateAsync_ShouldReturnSessionsForMatchingVehicle()
    {
        var matchingVehicle = CreateVehicle("KR12345");
        var otherVehicle = CreateVehicle("WX54321");

        await _repo.AddAsync(CreateSession(matchingVehicle, isActive: true));
        await _repo.AddAsync(CreateSession(matchingVehicle, isActive: false));
        await _repo.AddAsync(CreateSession(otherVehicle, isActive: true));

        var result = (await _repo.FindByLicensePlateAsync("kr12345")).ToList();

        Assert.Equal(2, result.Count);
        Assert.All(result, session => Assert.Equal("KR12345", session.Vehicle.LicensePlate));
    }

    [Fact]
    public async Task FindActiveSessionsAsync_ShouldReturnOnlyActiveSessions()
    {
        var vehicle = CreateVehicle("KR77777");

        await _repo.AddAsync(CreateSession(vehicle, isActive: true));
        await _repo.AddAsync(CreateSession(vehicle, isActive: false));

        var result = (await _repo.FindActiveSessionsAsync()).ToList();

        Assert.Single(result);
        Assert.True(result[0].IsActive);
    }

    [Fact]
    public async Task FindHistoryByLicensePlateAsync_ShouldReturnOnlyInactiveSessionsForVehicle()
    {
        var vehicle = CreateVehicle("KR22222");

        await _repo.AddAsync(CreateSession(vehicle, isActive: true));
        await _repo.AddAsync(CreateSession(vehicle, isActive: false));
        await _repo.AddAsync(CreateSession(CreateVehicle("KR33333"), isActive: false));

        var result = (await _repo.FindHistoryByLicensePlateAsync("KR22222")).ToList();

        Assert.Single(result);
        Assert.False(result[0].IsActive);
        Assert.Equal("KR22222", result[0].Vehicle.LicensePlate);
    }

    [Fact]
    public async Task FindByLicensePlateAsync_ShouldThrowArgumentException_WhenLicensePlateIsEmpty()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _repo.FindByLicensePlateAsync(string.Empty));
    }

    [Fact]
    public async Task FindHistoryByLicensePlateAsync_ShouldThrowArgumentException_WhenLicensePlateIsEmpty()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _repo.FindHistoryByLicensePlateAsync(" "));
    }

    private static Vehicle CreateVehicle(string licensePlate)
    {
        return new Vehicle
        {
            Id = Guid.NewGuid(),
            LicensePlate = licensePlate,
            Brand = "Audi",
            Color = "Black"
        };
    }

    private static ParkingSession CreateSession(Vehicle vehicle, bool isActive)
    {
        var gate = new ParkingGate
        {
            Id = Guid.NewGuid(),
            Name = isActive ? "Entry Gate" : "Exit Gate",
            Type = isActive ? GateType.Entry : GateType.Exit,
            Location = "Main",
            IsOperational = true
        };

        var tariff = new ParkingTariff
        {
            Id = Guid.NewGuid(),
            Name = "Standard",
            FreeParkingDuration = TimeSpan.FromMinutes(15),
            HourlyRate = 5m,
            DailyMaxRate = 50m,
            IsActive = true
        };

        return new ParkingSession
        {
            VehicleId = vehicle.Id,
            Vehicle = vehicle,
            ParkingGateId = gate.Id,
            ParkingGate = gate,
            ParkingTariffId = tariff.Id,
            ParkingTariff = tariff,
            GateName = gate.Name,
            EntryTime = DateTime.UtcNow.AddHours(-2),
            ExitTime = isActive ? null : DateTime.UtcNow,
            ParkingFee = isActive ? null : 10m,
            IsActive = isActive
        };
    }
}
