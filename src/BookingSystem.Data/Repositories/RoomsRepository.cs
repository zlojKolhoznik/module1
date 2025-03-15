// <copyright file="RoomsRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Data.Repositories;

using BookingSystem.Data.Abstractions;
using BookingSystem.Data.Models;
using BookingSystem.Data.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

/// <inheritdoc cref="IRoomsRepository"/>
/// <param name="context">Database context that will be used in repository.</param>
public class RoomsRepository(IApplicationDbContext context) : IRoomsRepository
{
    /// <inheritdoc/>
    public async Task AddRoomAsync(Room room)
    {
        await context.Set<Room>().AddAsync(room);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Thrown when room with specified id does not exist.</exception>
    public async Task DeleteRoomAsync(Guid roomId)
    {
        var room = await context.Set<Room>().FindAsync(roomId)
            ?? throw new ArgumentException($"Room with id {roomId} not found.");
        context.Set<Room>().Remove(room);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Thrown when room with specified id does not exist.</exception>
    public async Task<Room> GetRoomByIdAsync(Guid roomId)
    {
        return await context.Set<Room>()
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(r => r.Id == roomId)
            ?? throw new ArgumentException($"Room with id {roomId} not found.");
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Room>> GetRoomsByHotelIdAsync(Guid hotelId)
    {
        return await context.Set<Room>()
            .Include(r => r.Hotel)
            .Where(r => r.HotelId == hotelId)
            .ToListAsync();
    }

    /// <inheritdoc/>
    public Task UpdateRoomAsync(Room room)
    {
        context.Set<Room>().Update(room);
        return Task.CompletedTask;
    }
}
