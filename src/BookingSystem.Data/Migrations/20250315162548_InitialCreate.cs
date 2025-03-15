// <copyright file="20250315162548_InitialCreate.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#nullable disable

namespace BookingSystem.Data.Migrations;

using Microsoft.EntityFrameworkCore.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Hotels",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                Address = table.Column<string>(type: "TEXT", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Hotels", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Rooms",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                Number = table.Column<int>(type: "INTEGER", nullable: false),
                Type = table.Column<int>(type: "INTEGER", nullable: false),
                Capacity = table.Column<int>(type: "INTEGER", nullable: false),
                HotelId = table.Column<Guid>(type: "TEXT", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Rooms", x => x.Id);
                table.ForeignKey(
                    name: "FK_Rooms_Hotels_HotelId",
                    column: x => x.HotelId,
                    principalTable: "Hotels",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Bookings",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                TenantName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                TenantPassportNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                TenantPhoneNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                Start = table.Column<DateTime>(type: "TEXT", nullable: false),
                End = table.Column<DateTime>(type: "TEXT", nullable: false),
                RoomId = table.Column<Guid>(type: "TEXT", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Bookings", x => x.Id);
                table.ForeignKey(
                    name: "FK_Bookings_Rooms_RoomId",
                    column: x => x.RoomId,
                    principalTable: "Rooms",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Bookings_RoomId",
            table: "Bookings",
            column: "RoomId");

        migrationBuilder.CreateIndex(
            name: "IX_Rooms_HotelId",
            table: "Rooms",
            column: "HotelId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Bookings");

        migrationBuilder.DropTable(
            name: "Rooms");

        migrationBuilder.DropTable(
            name: "Hotels");
    }
}
