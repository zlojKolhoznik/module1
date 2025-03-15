// <copyright file="RoomsService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Core.Services;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BookingSystem.Core.Models.Room;
using BookingSystem.Core.Services.Abstractions;
using BookingSystem.Data.Models;
using BookingSystem.Data.UnitOfWork.Abstractions;

/// <inheritdoc/>
/// <param name="unitOfWork">Unit of work instance.</param>
/// <param name="mapper">Mapper instance.</param>
public class RoomsService(IUnitOfWork unitOfWork, IMapper mapper) : IRoomsService
{
    /// <inheritdoc/>
    public async Task AddRoomAsync(CreateRoomDto room)
    {
        var roomEntity = mapper.Map<Room>(room);
        await unitOfWork.Rooms.AddRoomAsync(roomEntity);
        await unitOfWork.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task DeleteRoomAsync(Guid id)
    {
        await unitOfWork.Rooms.DeleteRoomAsync(id);
        await unitOfWork.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<FullRoomDto> GetRoomAsync(Guid id)
    {
        var room = await unitOfWork.Rooms.GetRoomByIdAsync(id);
        return mapper.Map<FullRoomDto>(room);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<BriefRoomDto>> GetRoomsAsync(Guid hotelId)
    {
        var rooms = await unitOfWork.Rooms.GetRoomsByHotelIdAsync(hotelId);
        return mapper.Map<IEnumerable<BriefRoomDto>>(rooms);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Thrown when ID of the room does not match the ID of the room to be updated.</exception>
    public async Task UpdateRoomAsync(Guid id, UpdateRoomDto room)
    {
        if (id != room.Id)
        {
            throw new ArgumentException("ID of the room does not match the ID of the room to be updated.");
        }

        var roomEntity = await unitOfWork.Rooms.GetRoomByIdAsync(id);
        mapper.Map(room, roomEntity);
        await unitOfWork.Rooms.UpdateRoomAsync(roomEntity);
        await unitOfWork.SaveChangesAsync();
    }
}
