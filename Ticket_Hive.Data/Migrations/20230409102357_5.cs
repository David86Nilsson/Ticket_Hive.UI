using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticket_Hive.Data.Migrations
{
    /// <inheritdoc />
    public partial class _5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventModelId",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateTime", "EventModelId", "Image" },
                values: new object[] { new DateTime(2023, 4, 19, 12, 23, 57, 509, DateTimeKind.Local).AddTicks(5690), null, null });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateTime", "EventModelId", "Image" },
                values: new object[] { new DateTime(2023, 4, 14, 12, 23, 57, 509, DateTimeKind.Local).AddTicks(5739), null, null });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateTime", "EventModelId", "Image" },
                values: new object[] { new DateTime(2023, 4, 29, 12, 23, 57, 509, DateTimeKind.Local).AddTicks(5742), null, null });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateTime", "EventModelId", "Image" },
                values: new object[] { new DateTime(2023, 4, 11, 12, 23, 57, 509, DateTimeKind.Local).AddTicks(5745), null, null });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateTime", "EventModelId", "Image" },
                values: new object[] { new DateTime(2023, 5, 4, 12, 23, 57, 509, DateTimeKind.Local).AddTicks(5748), null, null });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Events");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2023, 4, 14, 18, 46, 14, 566, DateTimeKind.Local).AddTicks(4656));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2023, 4, 9, 18, 46, 14, 566, DateTimeKind.Local).AddTicks(4711));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateTime",
                value: new DateTime(2023, 4, 24, 18, 46, 14, 566, DateTimeKind.Local).AddTicks(4714));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateTime",
                value: new DateTime(2023, 4, 6, 18, 46, 14, 566, DateTimeKind.Local).AddTicks(4717));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateTime",
                value: new DateTime(2023, 4, 29, 18, 46, 14, 566, DateTimeKind.Local).AddTicks(4720));
        }
    }
}
