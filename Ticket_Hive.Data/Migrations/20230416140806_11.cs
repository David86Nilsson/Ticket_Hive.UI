using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticket_Hive.Data.Migrations
{
    /// <inheritdoc />
    public partial class _11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Events_EventModelId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_EventModelId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventModelId",
                table: "Events");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateTime", "Image" },
                values: new object[] { new DateTime(2023, 4, 26, 16, 8, 6, 246, DateTimeKind.Local).AddTicks(2817), "/Images/EventImages/image 1.png" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateTime", "Image" },
                values: new object[] { new DateTime(2023, 4, 21, 16, 8, 6, 246, DateTimeKind.Local).AddTicks(2902), "/Images/EventImages/image 2.png" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateTime", "Image" },
                values: new object[] { new DateTime(2023, 5, 6, 16, 8, 6, 246, DateTimeKind.Local).AddTicks(2909), "/Images/EventImages/image 3.png" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateTime", "Image" },
                values: new object[] { new DateTime(2023, 4, 18, 16, 8, 6, 246, DateTimeKind.Local).AddTicks(2917), "/Images/EventImages/image 4.png" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateTime", "Image" },
                values: new object[] { new DateTime(2023, 5, 11, 16, 8, 6, 246, DateTimeKind.Local).AddTicks(2924), "/Images/EventImages/image 5.png" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventModelId",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateTime", "EventModelId", "Image" },
                values: new object[] { new DateTime(2023, 4, 23, 10, 54, 24, 830, DateTimeKind.Local).AddTicks(53), null, "/Images/EventImages/image 1" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateTime", "EventModelId", "Image" },
                values: new object[] { new DateTime(2023, 4, 18, 10, 54, 24, 830, DateTimeKind.Local).AddTicks(120), null, "/Images/EventImages/image 2" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateTime", "EventModelId", "Image" },
                values: new object[] { new DateTime(2023, 5, 3, 10, 54, 24, 830, DateTimeKind.Local).AddTicks(124), null, "/Images/EventImages/image 3" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateTime", "EventModelId", "Image" },
                values: new object[] { new DateTime(2023, 4, 15, 10, 54, 24, 830, DateTimeKind.Local).AddTicks(128), null, "/Images/EventImages/image 4" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateTime", "EventModelId", "Image" },
                values: new object[] { new DateTime(2023, 5, 8, 10, 54, 24, 830, DateTimeKind.Local).AddTicks(132), null, "/Images/EventImages/image 5" });

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventModelId",
                table: "Events",
                column: "EventModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Events_EventModelId",
                table: "Events",
                column: "EventModelId",
                principalTable: "Events",
                principalColumn: "Id");
        }
    }
}
