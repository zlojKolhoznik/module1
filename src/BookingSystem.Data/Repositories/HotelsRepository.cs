// <copyright file="HotelsRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Data.Repositories;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingSystem.Data.Abstractions;
using BookingSystem.Data.Models;
using BookingSystem.Data.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

/// <inheritdoc cref="IHotelsRepository"/>
/// <param name="context">Database context that will be used in repository.</param>
public class HotelsRepository(IApplicationDbContext context)
    : IHotelsRepository
{
    /// <inheritdoc/>
    public async Task AddHotelAsync(Hotel hotel)
    {
        await context.Set<Hotel>().AddAsync(hotel);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Thrown when hotel with specified id does not exist.</exception>
    public async Task DeleteHotelAsync(Guid hotelId)
    {
        var hotel = await context.Set<Hotel>().FindAsync(hotelId)
            ?? throw new ArgumentException($"Hotel with id {hotelId} not found.");
        context.Set<Hotel>().Remove(hotel);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Thrown when hotel with specified id does not exist.</exception>
    public async Task<Hotel> GetHotelByIdAsync(Guid hotelId)
    {
        return await context.Set<Hotel>()
                .Include(h => h.Rooms)
                .FirstOrDefaultAsync(h => h.Id == hotelId)
            ?? throw new ArgumentException($"Hotel with id {hotelId} not found.");
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Thrown when hotel with specified id does not exist.</exception>
    public async Task<IEnumerable<Hotel>> GetHotelsAsync()
    {
        return await context.Set<Hotel>().ToListAsync();
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Thrown when hotel with specified id does not exist.</exception>
    public Task UpdateHotelAsync(Hotel hotel)
    {
        context.Set<Hotel>().Update(hotel);
        return Task.CompletedTask;
    }
}
