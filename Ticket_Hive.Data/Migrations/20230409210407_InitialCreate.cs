using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticket_Hive.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Events_EventModelId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_EventModelId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "EventModelId",
                table: "AppUsers");

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

            migrationBuilder.AddColumn<int>(
                name: "UserModelId",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppUserModelEventModel",
                columns: table => new
                {
                    EventsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserModelEventModel", x => new { x.EventsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_AppUserModelEventModel_AppUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserModelEventModel_Events_EventsId",
                        column: x => x.EventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserEvents",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    AppUserModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEvents", x => new { x.UserId, x.EventId });
                    table.ForeignKey(
                        name: "FK_UserEvents_AppUsers_AppUserModelId",
                        column: x => x.AppUserModelId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserEvents_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEvents_UserModel_UserId",
                        column: x => x.UserId,
                        principalTable: "UserModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateTime", "EventModelId", "Image", "UserModelId" },
                values: new object[] { new DateTime(2023, 4, 19, 23, 4, 7, 69, DateTimeKind.Local).AddTicks(9156), null, null, null });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateTime", "EventModelId", "Image", "UserModelId" },
                values: new object[] { new DateTime(2023, 4, 14, 23, 4, 7, 69, DateTimeKind.Local).AddTicks(9205), null, null, null });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateTime", "EventModelId", "Image", "UserModelId" },
                values: new object[] { new DateTime(2023, 4, 29, 23, 4, 7, 69, DateTimeKind.Local).AddTicks(9208), null, null, null });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateTime", "EventModelId", "Image", "UserModelId" },
                values: new object[] { new DateTime(2023, 4, 11, 23, 4, 7, 69, DateTimeKind.Local).AddTicks(9211), null, null, null });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateTime", "EventModelId", "Image", "UserModelId" },
                values: new object[] { new DateTime(2023, 5, 4, 23, 4, 7, 69, DateTimeKind.Local).AddTicks(9214), null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventModelId",
                table: "Events",
                column: "EventModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_UserModelId",
                table: "Events",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserModelEventModel_UsersId",
                table: "AppUserModelEventModel",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEvents_AppUserModelId",
                table: "UserEvents",
                column: "AppUserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEvents_EventId",
                table: "UserEvents",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Events_EventModelId",
                table: "Events",
                column: "EventModelId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_UserModel_UserModelId",
                table: "Events",
                column: "UserModelId",
                principalTable: "UserModel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Events_EventModelId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_UserModel_UserModelId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "AppUserModelEventModel");

            migrationBuilder.DropTable(
                name: "UserEvents");

            migrationBuilder.DropTable(
                name: "UserModel");

            migrationBuilder.DropIndex(
                name: "IX_Events_EventModelId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_UserModelId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventModelId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "EventModelId",
                table: "AppUsers",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventModelId",
                value: null);

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "EventModelId",
                value: null);

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

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_EventModelId",
                table: "AppUsers",
                column: "EventModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Events_EventModelId",
                table: "AppUsers",
                column: "EventModelId",
                principalTable: "Events",
                principalColumn: "Id");
        }
    }
}
