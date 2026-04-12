using ParkingApp.Core.Interfaces;
using ParkingApp.Core.Interfaces.Services;
using ParkingApp.Infrastructure.Memory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
