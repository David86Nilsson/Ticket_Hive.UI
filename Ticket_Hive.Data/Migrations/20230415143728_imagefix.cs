using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticket_Hive.Data.Migrations
{
    /// <inheritdoc />
    public partial class imagefix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateTime", "Image" },
                values: new object[] { new DateTime(2023, 4, 25, 16, 37, 28, 620, DateTimeKind.Local).AddTicks(692), "/Images/EventImages/image 1" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateTime", "Image" },
                values: new object[] { new DateTime(2023, 4, 20, 16, 37, 28, 620, DateTimeKind.Local).AddTicks(761), "/Images/EventImages/image 2" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateTime", "Image" },
                values: new object[] { new DateTime(2023, 5, 5, 16, 37, 28, 620, DateTimeKind.Local).AddTicks(765), "/Images/EventImages/image 3" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateTime", "Image" },
                values: new object[] { new DateTime(2023, 4, 17, 16, 37, 28, 620, DateTimeKind.Local).AddTicks(768), "/Images/EventImages/image 4" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateTime", "Image" },
                values: new object[] { new DateTime(2023, 5, 10, 16, 37, 28, 620, DateTimeKind.Local).AddTicks(771), "/Images/EventImages/image 5" });
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
