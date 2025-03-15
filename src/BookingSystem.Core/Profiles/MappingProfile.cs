// <copyright file="MappingProfile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Core.Profiles;

using AutoMapper;
using BookingSystem.Core.Models.Booking;
using BookingSystem.Core.Models.Hotel;
using BookingSystem.Core.Models.Room;
using BookingSystem.Data.Models;

/// <summary>
/// Represents a mapping profile for AutoMapper.
/// </summary>
public class MappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingProfile"/> class.
    /// </summary>
    public MappingProfile()
    {
        CreateBookingMaps();
        CreateRoomMaps();
        CreateHotelMaps();
    }

    private void CreateBookingMaps()
    {
        CreateMap<CreateBookingDto, Booking>();
        CreateMap<UpdateBookingDto, Booking>();
        CreateMap<Booking, FullBookingDto>();
        CreateMap<Booking, BriefBookingDto>();
    }

    private void CreateRoomMaps()
    {
        CreateMap<Room, FullRoomDto>();
        CreateMap<Room, BriefRoomDto>();
        CreateMap<CreateRoomDto, Room>();
        CreateMap<UpdateRoomDto, Room>();
    }

    private void CreateHotelMaps()
    {
        CreateMap<Hotel, FullHotelDto>();
        CreateMap<Hotel, BriefHotelDto>();
        CreateMap<CreateHotelDto, Hotel>();
        CreateMap<UpdateHotelDto, Hotel>();
    }
}
