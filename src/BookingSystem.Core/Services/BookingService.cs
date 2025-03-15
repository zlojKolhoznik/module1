// <copyright file="BookingService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Core.Services;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BookingSystem.Core.Models.Booking;
using BookingSystem.Core.Services.Abstractions;
using BookingSystem.Data.Models;
using BookingSystem.Data.UnitOfWork.Abstractions;

/// <inheritdoc/>
/// <param name="unitOfWork">Unit of work instance.</param>
/// <param name="mapper">Mapper instance.</param>
public class BookingService(IUnitOfWork unitOfWork, IMapper mapper) : IBookingService
{
    /// <inheritdoc/>
    public async Task AddBookingAsync(CreateBookingDto booking)
    {
        await EnsureRoomIsAvailable(booking.RoomId, booking.Start, booking.End);

        var bookingEntity = mapper.Map<Booking>(booking);
        await unitOfWork.Bookings.AddAsync(bookingEntity);
        await unitOfWork.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task DeleteBookingAsync(Guid id)
    {
        await unitOfWork.Bookings.DeleteAsync(id);
    }

    /// <inheritdoc/>
    public async Task<FullBookingDto> GetBookingAsync(Guid id)
    {
        var booking = await unitOfWork.Bookings.GetByIdAsync(id);
        return mapper.Map<FullBookingDto>(booking);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<BriefBookingDto>> GetBookingsAsync()
    {
        var bookings = await unitOfWork.Bookings.GetAllAsync();
        return mapper.Map<IEnumerable<BriefBookingDto>>(bookings);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Thrown when ID of the booking does not match the ID of the booking to be updated.</exception>
    public async Task UpdateBookingAsync(Guid id, UpdateBookingDto booking)
    {
        if (id != booking.Id)
        {
            throw new ArgumentException("ID of the booking does not match the ID of the booking to be updated.");
        }

        await EnsureRoomIsAvailable(booking.RoomId, booking.Start, booking.End);

        var bookingEntity = await unitOfWork.Bookings.GetByIdAsync(id);
        mapper.Map(booking, bookingEntity);
        await unitOfWork.Bookings.UpdateAsync(bookingEntity);
        await unitOfWork.SaveChangesAsync();
    }

    private async Task EnsureRoomIsAvailable(
        Guid roomId,
        DateTime start,
        DateTime end)
    {
        var room = await unitOfWork.Rooms.GetRoomByIdAsync(roomId)
                    ?? throw new ArgumentException("Room does not exist.");

        if (room.Bookings.Any(b => b.Start <= end && b.End >= start))
        {
            throw new ArgumentException("Room is already booked for specified period.");
        }
    }
}
