using ParkingApp.Core;
using ParkingApp.Core.Interfaces;
using ParkingApp.Core.Interfaces.Services;
using ParkingApp.Infrastructure.Memory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCoreServices();

builder.Services.AddOpenApi();

// Repozytoria
builder.Services.AddSingleton<IVehicleRepository, MemoryVehicleRepository>();
builder.Services.AddSingleton<IParkingGateRepository, MemoryParkingGateRepository>();
builder.Services.AddSingleton<IParkingSessionRepository, MemoryParkingSessionRepository>();
builder.Services.AddSingleton<IParkingTariffRepository, MemoryParkingTariffRepository>();
builder.Services.AddSingleton<ICameraCaptureRepository, MemoryCameraCaptureRepository>();

// Unit of Work
builder.Services.AddSingleton<IParkingUnitOfWork, MemoryParkingUnitOfWork>();

// Serwisy
builder.Services.AddSingleton<IParkingGateService, MemoryParkingGateService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
