using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticket_Hive.Data.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2023, 4, 21, 10, 48, 39, 776, DateTimeKind.Local).AddTicks(7629));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2023, 4, 16, 10, 48, 39, 776, DateTimeKind.Local).AddTicks(7685));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateTime",
                value: new DateTime(2023, 5, 1, 10, 48, 39, 776, DateTimeKind.Local).AddTicks(7688));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateTime",
                value: new DateTime(2023, 4, 13, 10, 48, 39, 776, DateTimeKind.Local).AddTicks(7692));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateTime",
                value: new DateTime(2023, 5, 6, 10, 48, 39, 776, DateTimeKind.Local).AddTicks(7695));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2023, 4, 21, 7, 45, 49, 720, DateTimeKind.Local).AddTicks(3291));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2023, 4, 16, 7, 45, 49, 720, DateTimeKind.Local).AddTicks(3342));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateTime",
                value: new DateTime(2023, 5, 1, 7, 45, 49, 720, DateTimeKind.Local).AddTicks(3345));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateTime",
                value: new DateTime(2023, 4, 13, 7, 45, 49, 720, DateTimeKind.Local).AddTicks(3348));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateTime",
                value: new DateTime(2023, 5, 6, 7, 45, 49, 720, DateTimeKind.Local).AddTicks(3351));
        }
    }
}
