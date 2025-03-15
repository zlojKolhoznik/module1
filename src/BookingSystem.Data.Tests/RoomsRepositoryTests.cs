// <copyright file="RoomsRepositoryTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Data.Tests;

using BookingSystem.Data.Abstractions;
using BookingSystem.Data.Models;
using BookingSystem.Data.Repositories;
using Moq;
using Moq.EntityFrameworkCore;

public class RoomsRepositoryTests
{
    private readonly Mock<IApplicationDbContext> _mockContext;
    private readonly RoomsRepository _repository;

    public RoomsRepositoryTests()
    {
        _mockContext = new Mock<IApplicationDbContext>();
        _repository = new RoomsRepository(_mockContext.Object);
    }

    [Fact]
    public async Task AddRoomAsync_ShouldAddRoom()
    {
        var room = new Room { Id = Guid.NewGuid() };
        _mockContext.Setup(m => m.Set<Room>().AddAsync(room, It.IsAny<CancellationToken>()))
            .Verifiable();

        await _repository.AddRoomAsync(room);

        _mockContext.Verify(m => m.Set<Room>().AddAsync(room, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteRoomAsync_ShouldDeleteRoom_WhenRoomExists()
    {
        var roomId = Guid.NewGuid();
        var room = new Room { Id = roomId };

        _mockContext.Setup(m => m.Set<Room>().FindAsync(roomId)).ReturnsAsync(room);

        await _repository.DeleteRoomAsync(roomId);

        _mockContext.Verify(m => m.Set<Room>().Remove(room), Times.Once);
    }

    [Fact]
    public async Task DeleteRoomAsync_ShouldThrowArgumentException_WhenRoomDoesNotExist()
    {
        var roomId = Guid.NewGuid();

        _mockContext.Setup(m => m.Set<Room>().FindAsync(roomId))
            .ReturnsAsync((Room?)null);

        await Assert.ThrowsAsync<ArgumentException>(() => _repository.DeleteRoomAsync(roomId));
    }

    [Fact]
    public async Task GetRoomByIdAsync_ShouldReturnRoom_WhenRoomExists()
    {
        var roomId = Guid.NewGuid();
        var room = new Room { Id = roomId };
        var testList = new List<Room> { room };

        _mockContext.Setup(m => m.Set<Room>())
            .ReturnsDbSet(testList);

        var result = await _repository.GetRoomByIdAsync(roomId);

        Assert.Equal(room, result);
    }

    [Fact]
    public async Task GetRoomByIdAsync_ShouldThrowArgumentException_WhenRoomDoesNotExist()
    {
        var roomId = Guid.NewGuid();
        var testList = new List<Room>();

        _mockContext.Setup(m => m.Set<Room>())
            .ReturnsDbSet(testList);

        await Assert.ThrowsAsync<ArgumentException>(() => _repository.GetRoomByIdAsync(roomId));
    }

    [Fact]
    public async Task GetRoomsByHotelIdAsync_ShouldReturnRooms_WhenRoomsExist()
    {
        var hotelId = Guid.NewGuid();
        var rooms = new List<Room>
        {
            new() { Id = Guid.NewGuid(), HotelId = hotelId },
            new() { Id = Guid.NewGuid(), HotelId = hotelId },
        };

        _mockContext.Setup(m => m.Set<Room>())
            .ReturnsDbSet(rooms);

        var result = await _repository.GetRoomsByHotelIdAsync(hotelId);

        Assert.Equal(rooms, result);
    }

    [Fact]
    public async Task UpdateRoomAsync_ShouldUpdateRoom()
    {
        var room = new Room { Id = Guid.NewGuid() };
        _mockContext.Setup(m => m.Set<Room>().Update(room))
            .Verifiable();

        await _repository.UpdateRoomAsync(room);

        _mockContext.Verify(m => m.Set<Room>().Update(room), Times.Once);
    }
}
