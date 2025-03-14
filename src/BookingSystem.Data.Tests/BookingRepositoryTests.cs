// <copyright file="BookingRepositoryTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Data.Tests;

using System;
using BookingSystem.Data.Abstractions;
using BookingSystem.Data.Models;
using BookingSystem.Data.Repositories;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

public class BookingRepositoryTests
{
    private readonly Mock<IApplicationDbContext> _mockContext;
    private readonly BookingRepository _repository;

    public BookingRepositoryTests()
    {
        _mockContext = new Mock<IApplicationDbContext>();
        _repository = new BookingRepository(_mockContext.Object);
    }

    [Fact]
    public async Task AddAsync_ShouldAddBooking()
    {
        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            TenantName = "John Doe",
            TenantPassportNumber = "123456789",
            TenantPhoneNumber = "555-1234",
        };
        _mockContext.Setup(m => m.Set<Booking>().AddAsync(booking, It.IsAny<CancellationToken>()))
            .Verifiable();

        await _repository.AddAsync(booking);

        _mockContext.Verify(m => m.Set<Booking>().AddAsync(booking, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteBooking_WhenBookingExists()
    {
        var bookingId = Guid.NewGuid();
        var booking = new Booking
        {
            Id = bookingId,
            TenantName = "John Doe",
            TenantPassportNumber = "123456789",
            TenantPhoneNumber = "555-1234",
        };
        var testList = new List<Booking> { booking };

        _mockContext.Setup(m => m.Set<Booking>())
            .ReturnsDbSet(testList);

        await _repository.DeleteAsync(bookingId);

        _mockContext.Verify(m => m.Set<Booking>().Remove(booking), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowArgumentException_WhenBookingDoesNotExist()
    {
        var bookingId = Guid.NewGuid();
        var testList = new List<Booking>();

        _mockContext.Setup(m => m.Set<Booking>())
            .ReturnsDbSet(testList);

        await Assert.ThrowsAsync<ArgumentException>(() => _repository.DeleteAsync(bookingId));
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnBookings()
    {
        var bookings = new List<Booking>
        {
            new()
            {
                Id = Guid.NewGuid(),
                TenantName = "John Doe",
                TenantPassportNumber = "123456789",
                TenantPhoneNumber = "555-1234",
            },
            new()
            {
                Id = Guid.NewGuid(),
                TenantName = "Jane Doe",
                TenantPassportNumber = "987654321",
                TenantPhoneNumber = "555-5678",
            },
        };

        _mockContext.Setup(m => m.Set<Booking>())
            .ReturnsDbSet(bookings);

        var result = await _repository.GetAllAsync();

        Assert.Equal(bookings, result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnBooking_WhenBookingExists()
    {
        var bookingId = Guid.NewGuid();
        var booking = new Booking
        {
            Id = bookingId,
            TenantName = "John Doe",
            TenantPassportNumber = "123456789",
            TenantPhoneNumber = "555-1234",
        };
        var testList = new List<Booking> { booking };

        _mockContext.Setup(m => m.Set<Booking>())
            .ReturnsDbSet(testList);

        var result = await _repository.GetByIdAsync(bookingId);

        Assert.Equal(booking, result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowArgumentException_WhenBookingDoesNotExist()
    {
        var bookingId = Guid.NewGuid();
        var testList = new List<Booking>();

        _mockContext.Setup(m => m.Set<Booking>())
            .ReturnsDbSet(testList);

        await Assert.ThrowsAsync<ArgumentException>(() => _repository.GetByIdAsync(bookingId));
    }

    [Fact]
    public async Task GetByRoomIdAsync_ShouldReturnBookings_WhenBookingsExist()
    {
        var roomId = Guid.NewGuid();
        var bookings = new List<Booking>
        {
            new()
            {
                Id = Guid.NewGuid(),
                RoomId = roomId,
                TenantName = "John Doe",
                TenantPassportNumber = "123456789",
                TenantPhoneNumber = "555-1234",
            },
            new()
            {
                Id = Guid.NewGuid(),
                RoomId = roomId,
                TenantName = "Jane Doe",
                TenantPassportNumber = "987654321",
                TenantPhoneNumber = "555-5678",
            },
        };

        _mockContext.Setup(m => m.Set<Booking>())
            .ReturnsDbSet(bookings);

        var result = await _repository.GetByRoomIdAsync(roomId);

        Assert.Equal(bookings, result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateBooking()
    {
        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            TenantName = "John Doe",
            TenantPassportNumber = "123456789",
            TenantPhoneNumber = "555-1234",
        };

        _mockContext.Setup(m => m.Set<Booking>().Update(booking))
            .Verifiable();

        await _repository.UpdateAsync(booking);

        _mockContext.Verify(m => m.Set<Booking>().Update(booking), Times.Once);
    }
}
