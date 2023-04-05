using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ticket_Hive.Data.Migrations
{
    /// <inheritdoc />
    public partial class _4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Capacity", "DateTime", "EventType", "Location", "Name", "Price", "TicketsSold" },
                values: new object[,]
                {
                    { 1, 100, new DateTime(2023, 4, 14, 18, 46, 14, 566, DateTimeKind.Local).AddTicks(4656), "Nightclub", "Lund", "Kareoke bowling", 100m, 0 },
                    { 2, 300, new DateTime(2023, 4, 9, 18, 46, 14, 566, DateTimeKind.Local).AddTicks(4711), "Musical", "Malmö", "Mama Mia", 200m, 0 },
                    { 3, 30000, new DateTime(2023, 4, 24, 18, 46, 14, 566, DateTimeKind.Local).AddTicks(4714), "Sport", "Stockholm", "AIK - Hammarby", 500m, 0 },
                    { 4, 20, new DateTime(2023, 4, 6, 18, 46, 14, 566, DateTimeKind.Local).AddTicks(4717), "Övrigt", "Halmstad", "Gästföreläsning med Steve Jobs", 10m, 20 },
                    { 5, 10, new DateTime(2023, 4, 29, 18, 46, 14, 566, DateTimeKind.Local).AddTicks(4720), "Sport", "Köpenhamn", "VM i Rally-Pingis", 25m, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
