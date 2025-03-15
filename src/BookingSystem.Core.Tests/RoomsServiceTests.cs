// <copyright file="RoomsServiceTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Core.Tests;

using AutoMapper;
using BookingSystem.Core.Models.Booking;
using BookingSystem.Core.Models.Hotel;
using BookingSystem.Core.Models.Room;
using BookingSystem.Core.Services;
using BookingSystem.Data.Models;
using BookingSystem.Data.UnitOfWork.Abstractions;
using Moq;

public class RoomsServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly RoomsService _roomsService;

    public RoomsServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _roomsService = new RoomsService(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task AddRoomAsync_ShouldAddRoom()
    {
        // Arrange
        var createRoomDto = new CreateRoomDto
        {
            Number = 101,
            Type = RoomType.Standard,
            Capacity = RoomCapacity.Double,
            HotelId = Guid.NewGuid(),
        };
        var room = new Room
        {
            Number = 101,
            Type = RoomType.Standard,
            Capacity = RoomCapacity.Double,
            HotelId = createRoomDto.HotelId,
        };
        _mapperMock.Setup(m => m.Map<Room>(createRoomDto)).Returns(room);
        _unitOfWorkMock.Setup(u => u.Rooms.AddRoomAsync(room)).Verifiable();
        _unitOfWorkMock.Setup(u => u.Hotels.GetHotelByIdAsync(createRoomDto.HotelId));

        // Act
        await _roomsService.AddRoomAsync(createRoomDto);

        // Assert
        _unitOfWorkMock.Verify(u => u.Rooms.AddRoomAsync(room), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteRoomAsync_ShouldDeleteRoom()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        _unitOfWorkMock.Setup(u => u.Rooms.DeleteRoomAsync(roomId)).Verifiable();

        // Act
        await _roomsService.DeleteRoomAsync(roomId);

        // Assert
        _unitOfWorkMock.Verify(u => u.Rooms.DeleteRoomAsync(roomId), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task GetRoomAsync_ShouldReturnRoom()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var room = new Room
        {
            Id = roomId,
            Number = 101,
            Type = RoomType.Standard,
            Capacity = RoomCapacity.Double,
            HotelId = Guid.NewGuid(),
        };
        var fullRoomDto = new FullRoomDto
        {
            Id = roomId,
            Number = 101,
            Type = RoomType.Standard,
            Capacity = RoomCapacity.Double,
            Hotel = new BriefHotelDto { Name = "Hotel", Address = "Address" },
            Bookings = [],
        };
        _unitOfWorkMock.Setup(u => u.Rooms.GetRoomByIdAsync(roomId)).ReturnsAsync(room);
        _mapperMock.Setup(m => m.Map<FullRoomDto>(room)).Returns(fullRoomDto);

        // Act
        var result = await _roomsService.GetRoomAsync(roomId);

        // Assert
        Assert.Equal(fullRoomDto, result);
    }

    [Fact]
    public async Task GetRoomsAsync_ShouldReturnRooms()
    {
        // Arrange
        var hotelId = Guid.NewGuid();
        var rooms = new List<Room>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Number = 101,
                Type = RoomType.Standard,
                Capacity = RoomCapacity.Double,
                HotelId = hotelId,
            },
            new()
            {
                Id = Guid.NewGuid(),
                Number = 102,
                Type = RoomType.Deluxe,
                Capacity = RoomCapacity.Triple,
                HotelId = hotelId,
            },
        };
        var briefRoomDtos = new List<BriefRoomDto>
        {
            new()
            {
                Id = rooms[0].Id,
                Number = 101,
                Type = RoomType.Standard,
                Capacity = RoomCapacity.Double,
                HotelId = hotelId,
            },
            new()
            {
                Id = rooms[1].Id,
                Number = 102,
                Type = RoomType.Deluxe,
                Capacity = RoomCapacity.Triple,
                HotelId = hotelId,
            },
        };
        _unitOfWorkMock.Setup(u => u.Rooms.GetRoomsByHotelIdAsync(hotelId)).ReturnsAsync(rooms);
        _mapperMock.Setup(m => m.Map<IEnumerable<BriefRoomDto>>(rooms)).Returns(briefRoomDtos);

        // Act
        var result = await _roomsService.GetRoomsAsync(hotelId);

        // Assert
        Assert.Equal(briefRoomDtos, result);
    }

    [Fact]
    public async Task UpdateRoomAsync_ShouldUpdateRoom()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var updateRoomDto = new UpdateRoomDto
        {
            Id = roomId,
            Number = 101,
            Type = RoomType.Standard,
            Capacity = RoomCapacity.Double,
        };
        var room = new Room
        {
            Id = roomId,
            Number = 101,
            Type = RoomType.Standard,
            Capacity = RoomCapacity.Double,
        };
        _unitOfWorkMock.Setup(u => u.Rooms.GetRoomByIdAsync(roomId)).ReturnsAsync(room);

        // Act
        await _roomsService.UpdateRoomAsync(roomId, updateRoomDto);

        // Assert
        _mapperMock.Verify(m => m.Map(updateRoomDto, room), Times.Once);
        _unitOfWorkMock.Verify(u => u.Rooms.UpdateRoomAsync(room), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateRoomAsync_ShouldThrowArgumentException_WhenIdsDoNotMatch()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var updateRoomDto = new UpdateRoomDto
        {
            Id = Guid.NewGuid(),
            Number = 101,
            Type = RoomType.Standard,
            Capacity = RoomCapacity.Double,
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _roomsService.UpdateRoomAsync(roomId, updateRoomDto));
    }
}