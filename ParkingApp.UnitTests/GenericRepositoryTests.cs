using ParkingApp.Core.Entities;
using ParkingApp.Core.Interfaces;
using ParkingApp.Infrastructure.Memory;

namespace ParkingApp.UnitTests.Repositories;

public class GenericRepositoryTests
{
    private readonly IGenericRepositoryAsync<Vehicle> _repo;

    public GenericRepositoryTests()
    {
        _repo = new MemoryVehicleRepository();
    }

    private static Vehicle CreateVehicle(string licensePlate, string brand = "Audi", string color = "Black")
    {
        return new Vehicle
        {
            LicensePlate = licensePlate,
            Brand = brand,
            Color = color
        };
    }

    [Fact]
    public async Task AddAsync_ShouldAddVehicleToRepository()
    {
        // Arrange
        var vehicle = CreateVehicle("TK8434Y");

        // Act
        await _repo.AddAsync(vehicle);
        var actual = await _repo.FindByIdAsync(vehicle.Id);

        // Assert
        Assert.NotNull(actual);
        Assert.Equal(vehicle.Id, actual!.Id);
        Assert.Equal(vehicle.LicensePlate, actual.LicensePlate);
        Assert.Equal(vehicle.Brand, actual.Brand);
        Assert.Equal(vehicle.Color, actual.Color);
    }

    [Fact]
    public async Task AddAsync_ShouldGenerateId_WhenEntityHasEmptyId()
    {
        // Arrange
        var vehicle = CreateVehicle("KR12345", "BMW", "Blue");
        vehicle.Id = Guid.Empty;

        // Act
        var result = await _repo.AddAsync(vehicle);

        // Assert
        Assert.NotEqual(Guid.Empty, result.Id);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowArgumentNullException_WhenEntityIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _repo.AddAsync(null!));
    }

    [Fact]
    public async Task AddAsync_ShouldThrowInvalidOperationException_WhenEntityWithSameIdAlreadyExists()
    {
        // Arrange
        var id = Guid.NewGuid();

        var vehicle1 = new Vehicle
        {
            Id = id,
            LicensePlate = "KR11111",
            Brand = "Audi",
            Color = "Black"
        };

        var vehicle2 = new Vehicle
        {
            Id = id,
            LicensePlate = "KR22222",
            Brand = "BMW",
            Color = "White"
        };

        await _repo.AddAsync(vehicle1);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _repo.AddAsync(vehicle2));
    }

    [Fact]
    public async Task FindByIdAsync_ShouldReturnEntity_WhenEntityExists()
    {
        // Arrange
        var vehicle = CreateVehicle("KR54321", "Toyota", "Silver");

        await _repo.AddAsync(vehicle);

        // Act
        var result = await _repo.FindByIdAsync(vehicle.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(vehicle.Id, result!.Id);
    }

    [Fact]
    public async Task FindByIdAsync_ShouldReturnNull_WhenEntityDoesNotExist()
    {
        // Act
        var result = await _repo.FindByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task FindAllAsync_ShouldReturnAllEntities()
    {
        // Arrange
        await _repo.AddAsync(CreateVehicle("KR10001"));
        await _repo.AddAsync(CreateVehicle("KR10002", "BMW", "White"));

        // Act
        var result = await _repo.FindAllAsync();

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task FindPagedAsync_ShouldReturnFirstPageCorrectly()
    {
        // Arrange
        for (int i = 1; i <= 5; i++)
        {
            await _repo.AddAsync(new Vehicle
            {
                LicensePlate = $"KR{i:00000}",
                Brand = "Brand",
                Color = "Color"
            });
        }

        // Act
        var result = await _repo.FindPagedAsync(1, 2);

        // Assert
        Assert.Equal(2, result.Items.Count);
        Assert.Equal(5, result.TotalCount);
        Assert.Equal(1, result.Page);
        Assert.Equal(2, result.PageSize);
        Assert.Equal(3, result.TotalPages);
        Assert.True(result.HasNext);
        Assert.False(result.HasPrevious);
        Assert.Equal(new[] { "KR00001", "KR00002" }, result.Items.Select(vehicle => vehicle.LicensePlate));
    }

    [Fact]
    public async Task FindPagedAsync_ShouldReturnSecondPageCorrectly()
    {
        // Arrange
        for (int i = 1; i <= 5; i++)
        {
            await _repo.AddAsync(new Vehicle
            {
                LicensePlate = $"TK{i:00000}",
                Brand = "Brand",
                Color = "Color"
            });
        }

        // Act
        var result = await _repo.FindPagedAsync(2, 2);

        // Assert
        Assert.Equal(2, result.Items.Count);
        Assert.Equal(2, result.Page);
        Assert.True(result.HasNext);
        Assert.True(result.HasPrevious);
        Assert.Equal(new[] { "TK00003", "TK00004" }, result.Items.Select(vehicle => vehicle.LicensePlate));
    }

    [Fact]
    public async Task FindPagedAsync_ShouldThrowArgumentException_WhenPageIsLessThanOrEqualToZero()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _repo.FindPagedAsync(0, 10));
    }

    [Fact]
    public async Task FindPagedAsync_ShouldThrowArgumentException_WhenPageSizeIsLessThanOrEqualToZero()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _repo.FindPagedAsync(1, 0));
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateExistingEntity()
    {
        // Arrange
        var vehicle = CreateVehicle("KR77777");

        await _repo.AddAsync(vehicle);

        vehicle.Color = "Red";
        vehicle.Brand = "BMW";

        // Act
        await _repo.UpdateAsync(vehicle);
        var updated = await _repo.FindByIdAsync(vehicle.Id);

        // Assert
        Assert.NotNull(updated);
        Assert.Equal("Red", updated!.Color);
        Assert.Equal("BMW", updated.Brand);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowArgumentNullException_WhenEntityIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _repo.UpdateAsync(null!));
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowArgumentException_WhenEntityIdIsEmpty()
    {
        // Arrange
        var vehicle = CreateVehicle("KR99999");
        vehicle.Id = Guid.Empty;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _repo.UpdateAsync(vehicle));
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowKeyNotFoundException_WhenEntityDoesNotExist()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            Id = Guid.NewGuid(),
            LicensePlate = "KR33333",
            Brand = "Audi",
            Color = "Black"
        };

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _repo.UpdateAsync(vehicle));
    }

    [Fact]
    public async Task RemoveByIdAsync_ShouldRemoveExistingEntity()
    {
        // Arrange
        var vehicle = CreateVehicle("KR55555");

        await _repo.AddAsync(vehicle);

        // Act
        await _repo.RemoveByIdAsync(vehicle.Id);
        var result = await _repo.FindByIdAsync(vehicle.Id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task RemoveByIdAsync_ShouldThrowArgumentException_WhenIdIsEmpty()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _repo.RemoveByIdAsync(Guid.Empty));
    }

    [Fact]
    public async Task RemoveByIdAsync_ShouldThrowKeyNotFoundException_WhenEntityDoesNotExist()
    {
        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _repo.RemoveByIdAsync(Guid.NewGuid()));
    }
}
