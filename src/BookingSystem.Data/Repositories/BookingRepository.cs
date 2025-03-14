// <copyright file="BookingRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace BookingSystem.Data.Repositories;

using BookingSystem.Data.Abstractions;
using BookingSystem.Data.Models;
using BookingSystem.Data.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

/// <inheritdoc />
/// <param name="context">Database context for the repository.</param>
public class BookingRepository(IApplicationDbContext context) : IBookingRepository
{
    /// <inheritdoc />
    public async Task AddAsync(Booking booking)
    {
        await context.Set<Booking>().AddAsync(booking);
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentException">Thrown when the booking is not found.</exception>
    public async Task DeleteAsync(Guid id)
    {
        var booking = await GetByIdAsync(id);
        context.Set<Booking>().Remove(booking);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Booking>> GetAllAsync()
    {
        return await context.Set<Booking>().ToListAsync();
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentException">Thrown when the booking is not found.</exception>
    public async Task<Booking> GetByIdAsync(Guid id)
    {
        return await context.Set<Booking>()
            .Include(b => b.Room)
            .SingleOrDefaultAsync(b => b.Id == id)
            ?? throw new ArgumentException("Booking not found.");
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Booking>> GetByRoomIdAsync(Guid roomId)
    {
        return await context.Set<Booking>()
            .Include(b => b.Room)
            .Where(b => b.RoomId == roomId)
            .ToListAsync();
    }

    /// <inheritdoc />
    public Task UpdateAsync(Booking booking)
    {
        context.Set<Booking>().Update(booking);
        return Task.CompletedTask;
    }
}
