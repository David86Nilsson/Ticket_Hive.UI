using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticket_Hive.Data.Migrations
{
    /// <inheritdoc />
    public partial class _10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateTime", "Image" },
                values: new object[] { new DateTime(2023, 4, 23, 10, 54, 24, 830, DateTimeKind.Local).AddTicks(53), "/Images/EventImages/image 1" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateTime", "Image" },
                values: new object[] { new DateTime(2023, 4, 18, 10, 54, 24, 830, DateTimeKind.Local).AddTicks(120), "/Images/EventImages/image 2" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateTime", "Image" },
                values: new object[] { new DateTime(2023, 5, 3, 10, 54, 24, 830, DateTimeKind.Local).AddTicks(124), "/Images/EventImages/image 3" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateTime", "Image" },
                values: new object[] { new DateTime(2023, 4, 15, 10, 54, 24, 830, DateTimeKind.Local).AddTicks(128), "/Images/EventImages/image 4" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateTime", "Image" },
                values: new object[] { new DateTime(2023, 5, 8, 10, 54, 24, 830, DateTimeKind.Local).AddTicks(132), "/Images/EventImages/image 5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateTime", "Image" },
                values: new object[] { new DateTime(2023, 4, 21, 15, 50, 46, 435, DateTimeKind.Local).AddTicks(5100), null });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateTime", "Image" },
                values: new object[] { new DateTime(2023, 4, 16, 15, 50, 46, 435, DateTimeKind.Local).AddTicks(5160), null });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateTime", "Image" },
                values: new object[] { new DateTime(2023, 5, 1, 15, 50, 46, 435, DateTimeKind.Local).AddTicks(5163), null });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateTime", "Image" },
                values: new object[] { new DateTime(2023, 4, 13, 15, 50, 46, 435, DateTimeKind.Local).AddTicks(5166), null });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateTime", "Image" },
                values: new object[] { new DateTime(2023, 5, 6, 15, 50, 46, 435, DateTimeKind.Local).AddTicks(5168), null });
        }
    }
}
