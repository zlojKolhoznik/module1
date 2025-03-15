// <copyright file="HotelsService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Core.Services;

using AutoMapper;
using BookingSystem.Core.Models.Hotel;
using BookingSystem.Core.Services.Abstractions;
using BookingSystem.Data.Models;
using BookingSystem.Data.UnitOfWork.Abstractions;

/// <inheritdoc/>
/// <param name="unitOfWork">Unit of work instance.</param>
/// <param name="mapper">Mapper instance.</param>
public class HotelsService(IUnitOfWork unitOfWork, IMapper mapper) : IHotelsService
{
    /// <inheritdoc/>
    public async Task AddHotelAsync(CreateHotelDto hotel)
    {
        var hotelEntity = mapper.Map<Hotel>(hotel);
        await unitOfWork.Hotels.AddHotelAsync(hotelEntity);
        await unitOfWork.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task DeleteHotelAsync(Guid id)
    {
        await unitOfWork.Hotels.DeleteHotelAsync(id);
        await unitOfWork.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<FullHotelDto> GetHotelAsync(Guid id)
    {
        var hotel = await unitOfWork.Hotels.GetHotelByIdAsync(id);
        return mapper.Map<FullHotelDto>(hotel);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<BriefHotelDto>> GetHotelsAsync()
    {
        var hotels = await unitOfWork.Hotels.GetHotelsAsync();
        return mapper.Map<IEnumerable<BriefHotelDto>>(hotels);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Thrown when ID of the hotel does not match the ID of the hotel to be updated.</exception>
    public async Task UpdateHotelAsync(Guid id, UpdateHotelDto hotel)
    {
        if (id != hotel.Id)
        {
            throw new ArgumentException("ID of the hotel does not match the ID of the hotel to be updated.");
        }

        var hotelEntity = await unitOfWork.Hotels.GetHotelByIdAsync(id);
        mapper.Map(hotel, hotelEntity);
        await unitOfWork.Hotels.UpdateHotelAsync(hotelEntity);
        await unitOfWork.SaveChangesAsync();
    }
}
