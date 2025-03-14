// <copyright file="UnitOfWork.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Data.UnitOfWork;

using BookingSystem.Data.Abstractions;
using BookingSystem.Data.Repositories.Abstractions;

/// <summary>
/// Unit of work class.
/// </summary>
/// <param name="hotels">Hotels repository.</param>
/// <param name="rooms">Rooms repository.</param>
/// <param name="context">Application database context.</param>
public class UnitOfWork(
    IHotelsRepository hotels,
    IRoomsRepository rooms,
    IApplicationDbContext context)
{
    /// <summary>
    /// Gets hotels repository.
    /// </summary>
    public IHotelsRepository Hotels => hotels;

    /// <summary>
    /// Gets rooms repository.
    /// </summary>
    public IRoomsRepository Rooms => rooms;

    /// <summary>
    /// Saves changes to the database.
    /// </summary>
    /// <returns>Number of affected rows.</returns>
    public Task<int> SaveChangesAsync()
    {
        return context.SaveChangesAsync();
    }
}
