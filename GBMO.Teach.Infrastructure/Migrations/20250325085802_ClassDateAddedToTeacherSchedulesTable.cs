using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GBMO.Teach.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClassDateAddedToTeacherSchedulesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3ad54497-b15f-4fdf-a534-1a83aa4dac0e"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6a5bb63b-f1cb-45a2-bb3c-73e195ec072d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a8c3a133-3b9a-4201-b417-29f10aa1d72b"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d5b7ae40-7797-4480-ad53-0ccc66c47173"));

            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "TeacherSchedules");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "TeacherSchedules");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "TeacherSchedules");

            migrationBuilder.AlterColumn<bool>(
                name: "IsBooked",
                table: "TeacherSchedules",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<DateTime>(
                name: "ClassEndDate",
                table: "TeacherSchedules",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ClassStartDate",
                table: "TeacherSchedules",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreationTime", "DeletedTime", "IsDeleted", "ModifiedDate", "RoleTypeId" },
                values: new object[,]
                {
                    { new Guid("329c5495-4cf7-4442-aba8-c50b0e667e22"), new DateTime(2025, 3, 25, 8, 58, 1, 997, DateTimeKind.Utc).AddTicks(9427), null, false, null, 2 },
                    { new Guid("6c9f19ea-729b-4336-ab92-13e687f64072"), new DateTime(2025, 3, 25, 8, 58, 1, 997, DateTimeKind.Utc).AddTicks(9425), null, false, null, 1 },
                    { new Guid("8d3f6b6d-70f8-4a75-9d1c-f8e651c9180e"), new DateTime(2025, 3, 25, 8, 58, 1, 997, DateTimeKind.Utc).AddTicks(9397), null, false, null, 0 },
                    { new Guid("a2fe89c9-91c6-4363-8120-bf7cb231fc49"), new DateTime(2025, 3, 25, 8, 58, 1, 997, DateTimeKind.Utc).AddTicks(9428), null, false, null, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("329c5495-4cf7-4442-aba8-c50b0e667e22"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6c9f19ea-729b-4336-ab92-13e687f64072"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d3f6b6d-70f8-4a75-9d1c-f8e651c9180e"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a2fe89c9-91c6-4363-8120-bf7cb231fc49"));

            migrationBuilder.DropColumn(
                name: "ClassEndDate",
                table: "TeacherSchedules");

            migrationBuilder.DropColumn(
                name: "ClassStartDate",
                table: "TeacherSchedules");

            migrationBuilder.AlterColumn<bool>(
                name: "IsBooked",
                table: "TeacherSchedules",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "TeacherSchedules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "TeacherSchedules",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "TeacherSchedules",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreationTime", "DeletedTime", "IsDeleted", "ModifiedDate", "RoleTypeId" },
                values: new object[,]
                {
                    { new Guid("3ad54497-b15f-4fdf-a534-1a83aa4dac0e"), new DateTime(2025, 3, 11, 8, 15, 2, 175, DateTimeKind.Utc).AddTicks(7740), null, false, null, 3 },
                    { new Guid("6a5bb63b-f1cb-45a2-bb3c-73e195ec072d"), new DateTime(2025, 3, 11, 8, 15, 2, 175, DateTimeKind.Utc).AddTicks(7737), null, false, null, 1 },
                    { new Guid("a8c3a133-3b9a-4201-b417-29f10aa1d72b"), new DateTime(2025, 3, 11, 8, 15, 2, 175, DateTimeKind.Utc).AddTicks(7709), null, false, null, 0 },
                    { new Guid("d5b7ae40-7797-4480-ad53-0ccc66c47173"), new DateTime(2025, 3, 11, 8, 15, 2, 175, DateTimeKind.Utc).AddTicks(7739), null, false, null, 2 }
                });
        }
    }
}
