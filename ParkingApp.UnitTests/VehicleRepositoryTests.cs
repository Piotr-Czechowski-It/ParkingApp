using ParkingApp.Core.Entities;
using ParkingApp.Core.Interfaces;
using ParkingApp.Infrastructure.Memory;

namespace ParkingApp.UnitTests.Repositories;

public class VehicleRepositoryTests
{
    private readonly IVehicleRepository _repo = new VehicleRepository();

    [Fact]
    public async Task FindByLicensePlateAsync_ShouldReturnVehicle_WhenLicensePlateExists()
    {
        var vehicle = new Vehicle
        {
            LicensePlate = "KR12345",
            Brand = "Audi",
            Color = "Black"
        };

        await _repo.AddAsync(vehicle);

        var result = await _repo.FindByLicensePlateAsync("kr12345");

        Assert.NotNull(result);
        Assert.Equal(vehicle.Id, result!.Id);
    }

    [Fact]
    public async Task FindByLicensePlateAsync_ShouldReturnNull_WhenLicensePlateDoesNotExist()
    {
        var result = await _repo.FindByLicensePlateAsync("NOPE123");

        Assert.Null(result);
    }

    [Fact]
    public async Task FindByLicensePlateAsync_ShouldThrowArgumentException_WhenLicensePlateIsEmpty()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _repo.FindByLicensePlateAsync(string.Empty));
    }
}
