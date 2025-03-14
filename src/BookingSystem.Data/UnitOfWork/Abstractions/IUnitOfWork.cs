// <copyright file="IUnitOfWork.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Data.UnitOfWork.Abstractions;

using BookingSystem.Data.Repositories.Abstractions;

/// <summary>
/// Unit of work interface.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Gets bookings repository.
    /// </summary>
    IBookingRepository Bookings { get; }

    /// <summary>
    /// Gets rooms repository.
    /// </summary>
    IRoomsRepository Rooms { get; }

    /// <summary>
    /// Gets hotels repository.
    /// </summary>
    IHotelsRepository Hotels { get; }

    /// <summary>
    /// Saves changes to the database.
    /// </summary>
    /// <returns>The number of rows affected.</returns>
    Task<int> SaveChangesAsync();
}
