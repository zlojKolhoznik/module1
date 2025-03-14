// <copyright file="IApplicationDbContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Data.Abstractions;

using Microsoft.EntityFrameworkCore;

/// <summary>
/// Application database context interface.
/// This interface is used to abstract the database context,
/// so that it can be easily mocked.
/// </summary>
public interface IApplicationDbContext : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the database set for the specified entity.
    /// </summary>
    /// <typeparam name="TEntity">Entity type to get set of.</typeparam>
    /// <returns>DbSet of TEntity.</returns>
    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Number of rows affected.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
