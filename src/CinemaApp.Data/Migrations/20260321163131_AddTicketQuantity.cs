using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Projections",
                keyColumn: "Id",
                keyValue: new Guid("4be1a7d8-cb46-4b08-9abc-016dd7a8bbb8"));

            migrationBuilder.DeleteData(
                table: "Projections",
                keyColumn: "Id",
                keyValue: new Guid("73a7d7c4-275a-4d92-88df-6fcf31721d6a"));

            migrationBuilder.DeleteData(
                table: "Projections",
                keyColumn: "Id",
                keyValue: new Guid("b642f67d-61c3-4fe9-a97d-add2657829d4"));

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Projections",
                columns: new[] { "Id", "AvailableTickets", "CinemaId", "IsDeleted", "MovieId", "Showtime", "TicketPrice" },
                values: new object[,]
                {
                    { new Guid("3ffc0a36-d5cb-4ac8-996a-2a9653114323"), 120, new Guid("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"), false, new Guid("ae50a5ab-9642-466f-b528-3cc61071bb4c"), new TimeOnly(18, 30, 0), 12.50m },
                    { new Guid("5eeb1a2a-f4c3-47ba-aeda-024b16fc3a88"), 80, new Guid("c3d4e5f6-a7b8-4c9d-0e1f-2a3b4c5d6e7f"), false, new Guid("68fb84b9-ef2a-402f-b4fc-595006f5c275"), new TimeOnly(16, 45, 0), 10.00m },
                    { new Guid("74107821-3eeb-401d-9e10-7f179e12644c"), 200, new Guid("b2c3d4e5-f6a7-4b8c-9d0e-1f2a3b4c5d6e"), false, new Guid("777634e2-3bb6-4748-8e91-7a10b70c78ac"), new TimeOnly(20, 0, 0), 14.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Projections",
                keyColumn: "Id",
                keyValue: new Guid("3ffc0a36-d5cb-4ac8-996a-2a9653114323"));

            migrationBuilder.DeleteData(
                table: "Projections",
                keyColumn: "Id",
                keyValue: new Guid("5eeb1a2a-f4c3-47ba-aeda-024b16fc3a88"));

            migrationBuilder.DeleteData(
                table: "Projections",
                keyColumn: "Id",
                keyValue: new Guid("74107821-3eeb-401d-9e10-7f179e12644c"));

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Tickets");

            migrationBuilder.InsertData(
                table: "Projections",
                columns: new[] { "Id", "AvailableTickets", "CinemaId", "IsDeleted", "MovieId", "Showtime", "TicketPrice" },
                values: new object[,]
                {
                    { new Guid("4be1a7d8-cb46-4b08-9abc-016dd7a8bbb8"), 200, new Guid("b2c3d4e5-f6a7-4b8c-9d0e-1f2a3b4c5d6e"), false, new Guid("777634e2-3bb6-4748-8e91-7a10b70c78ac"), new TimeOnly(20, 0, 0), 14.00m },
                    { new Guid("73a7d7c4-275a-4d92-88df-6fcf31721d6a"), 80, new Guid("c3d4e5f6-a7b8-4c9d-0e1f-2a3b4c5d6e7f"), false, new Guid("68fb84b9-ef2a-402f-b4fc-595006f5c275"), new TimeOnly(16, 45, 0), 10.00m },
                    { new Guid("b642f67d-61c3-4fe9-a97d-add2657829d4"), 120, new Guid("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"), false, new Guid("ae50a5ab-9642-466f-b528-3cc61071bb4c"), new TimeOnly(18, 30, 0), 12.50m }
                });
        }
    }
}
