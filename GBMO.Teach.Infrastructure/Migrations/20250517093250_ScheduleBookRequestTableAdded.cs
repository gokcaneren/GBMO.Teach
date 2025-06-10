using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GBMO.Teach.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ScheduleBookRequestTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1902a635-6625-4d35-bbaf-12de3bae0ada"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1984d501-efba-4469-a27e-9eb7d7a4ef0a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3c42d45d-9c47-45c4-b703-ff47158fe952"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b2c3782a-b50d-423a-aceb-be07ef2cc91c"));

            migrationBuilder.CreateTable(
                name: "ScheduleBookRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StudenId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uuid", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleBookRequests", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreationTime", "DeletedTime", "IsDeleted", "ModifiedDate", "RoleTypeId" },
                values: new object[,]
                {
                    { new Guid("13331b17-5829-4778-9967-c1d1da19b4c8"), new DateTime(2025, 5, 17, 9, 32, 48, 913, DateTimeKind.Utc).AddTicks(4012), null, false, null, 3 },
                    { new Guid("68720581-34ee-44f2-8d15-6c393eeb2828"), new DateTime(2025, 5, 17, 9, 32, 48, 913, DateTimeKind.Utc).AddTicks(4004), null, false, null, 1 },
                    { new Guid("92a6bf5c-2e1b-4438-8a68-001462a410bf"), new DateTime(2025, 5, 17, 9, 32, 48, 913, DateTimeKind.Utc).AddTicks(4008), null, false, null, 2 },
                    { new Guid("f32be637-a09c-4552-8df8-3a267d844df1"), new DateTime(2025, 5, 17, 9, 32, 48, 913, DateTimeKind.Utc).AddTicks(3952), null, false, null, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleBookRequests");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("13331b17-5829-4778-9967-c1d1da19b4c8"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("68720581-34ee-44f2-8d15-6c393eeb2828"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("92a6bf5c-2e1b-4438-8a68-001462a410bf"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f32be637-a09c-4552-8df8-3a267d844df1"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreationTime", "DeletedTime", "IsDeleted", "ModifiedDate", "RoleTypeId" },
                values: new object[,]
                {
                    { new Guid("1902a635-6625-4d35-bbaf-12de3bae0ada"), new DateTime(2025, 4, 8, 7, 41, 57, 330, DateTimeKind.Utc).AddTicks(5396), null, false, null, 1 },
                    { new Guid("1984d501-efba-4469-a27e-9eb7d7a4ef0a"), new DateTime(2025, 4, 8, 7, 41, 57, 330, DateTimeKind.Utc).AddTicks(5399), null, false, null, 3 },
                    { new Guid("3c42d45d-9c47-45c4-b703-ff47158fe952"), new DateTime(2025, 4, 8, 7, 41, 57, 330, DateTimeKind.Utc).AddTicks(5366), null, false, null, 0 },
                    { new Guid("b2c3782a-b50d-423a-aceb-be07ef2cc91c"), new DateTime(2025, 4, 8, 7, 41, 57, 330, DateTimeKind.Utc).AddTicks(5397), null, false, null, 2 }
                });
        }
    }
}
