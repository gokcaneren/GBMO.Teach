using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GBMO.Teach.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SubRequestTableadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("92afaefd-cec2-488e-9f2d-b1aeb017874d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("99291aab-3363-4185-9709-90da43c59618"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b61268ff-2862-4c7f-b8a3-5eefca9fdc58"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ff2e5149-0f77-4c8f-b545-d67a06d6e382"));

            migrationBuilder.CreateTable(
                name: "SubsRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StudenId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubsRequests", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubsRequests");

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

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreationTime", "DeletedTime", "IsDeleted", "ModifiedDate", "RoleTypeId" },
                values: new object[,]
                {
                    { new Guid("92afaefd-cec2-488e-9f2d-b1aeb017874d"), new DateTime(2025, 3, 31, 7, 6, 37, 412, DateTimeKind.Utc).AddTicks(2778), null, false, null, 1 },
                    { new Guid("99291aab-3363-4185-9709-90da43c59618"), new DateTime(2025, 3, 31, 7, 6, 37, 412, DateTimeKind.Utc).AddTicks(2732), null, false, null, 0 },
                    { new Guid("b61268ff-2862-4c7f-b8a3-5eefca9fdc58"), new DateTime(2025, 3, 31, 7, 6, 37, 412, DateTimeKind.Utc).AddTicks(2780), null, false, null, 3 },
                    { new Guid("ff2e5149-0f77-4c8f-b545-d67a06d6e382"), new DateTime(2025, 3, 31, 7, 6, 37, 412, DateTimeKind.Utc).AddTicks(2779), null, false, null, 2 }
                });
        }
    }
}
