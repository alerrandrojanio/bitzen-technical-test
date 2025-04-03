using FluentValidation;
using MeetingRooms.Application.Services;
using MeetingRooms.Domain.Entities;
using MeetingRooms.Domain.Interfaces;
using MeetingRooms.Infrastructure.Configurations;
using MeetingRooms.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MeetingRooms.DI.IoC;

public static class PipelineExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IReserveService, ReserveService>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IReserveRepository, ReserveRepository>();

        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
    }

    public static void ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(options =>
        {
            options.DefaultConnection = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        });

        services.Configure<AuthSettings>(configuration.GetSection(nameof(AuthSettings)));
    }

    public static void AddVersioning(this IServiceCollection services) 
    {
        services.AddApiVersioning(setup =>
        {
            setup.DefaultApiVersion = ApiVersion.Default;
            setup.AssumeDefaultVersionWhenUnspecified = true;
            setup.ReportApiVersions = true;
        });
    }

    public static IServiceCollection AddFluentValidators(this IServiceCollection services, Assembly assembly)
    {
        IEnumerable<Type?> validatorTypes = assembly.GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface && typeof(IValidator).IsAssignableFrom(t));

        foreach (var validatorType in validatorTypes)
        {
            Type? entityType = validatorType?.BaseType?.GetGenericArguments().FirstOrDefault();

            if (entityType != null)
            {
                var serviceType = typeof(IValidator<>).MakeGenericType(entityType);

                services.AddScoped(serviceType, validatorType!);
            }
        }

        return services;
    }

}
