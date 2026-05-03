using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using ParkingApp.Core.Validators;

namespace ParkingApp.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<ParkingGateValidator>();
        services.AddFluentValidationAutoValidation();

        return services;
    }
}
