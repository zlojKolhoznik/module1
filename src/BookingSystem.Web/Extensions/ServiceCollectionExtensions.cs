// <copyright file="ServiceCollectionExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Web.Extensions;

using BookingSystem.Core.Profiles;
using BookingSystem.Core.Services;
using BookingSystem.Core.Services.Abstractions;
using BookingSystem.Data;
using BookingSystem.Data.Abstractions;
using BookingSystem.Data.Repositories;
using BookingSystem.Data.Repositories.Abstractions;
using BookingSystem.Data.UnitOfWork;
using BookingSystem.Data.UnitOfWork.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

/// <summary>
/// Extension methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds documentation services.
    /// </summary>
    /// <param name="services">Service collection to add services to.</param>
    /// <returns><paramref name="services"/>.</returns>
    public static IServiceCollection AddDocumentation(this IServiceCollection services)
    {
        services.AddOpenApi();
        services.AddSwaggerGen(c =>
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }));

        return services;
    }

    /// <summary>
    /// Adds data access services.
    /// </summary>
    /// <param name="services">Service collection to add services to.</param>
    /// <param name="configuration">Configuration instance.</param>
    /// <returns><paramref name="services"/>.</returns>
    public static IServiceCollection AddDataAccess(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IRoomsRepository, RoomsRepository>();
        services.AddScoped<IHotelsRepository, HotelsRepository>();

        return services;
    }

    /// <summary>
    /// Adds AutoMapper.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
    /// <returns><paramref name="services"/>.</returns>
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(options => options.AddProfile<MappingProfile>());

        return services;
    }

    /// <summary>
    /// Adds domain services.
    /// </summary>
    /// <param name="services">Service collection to add services to.</param>
    /// <returns><paramref name="services"/>.</returns>
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IRoomsService, RoomsService>();
        services.AddScoped<IHotelsService, HotelsService>();

        return services;
    }
}
