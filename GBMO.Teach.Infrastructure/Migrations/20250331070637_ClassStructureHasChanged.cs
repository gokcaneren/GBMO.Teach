using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GBMO.Teach.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClassStructureHasChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassBookings");

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
                name: "IsBooked",
                table: "TeacherSchedules");

            migrationBuilder.AddColumn<int>(
                name: "ClassStatusses",
                table: "TeacherSchedules",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "TeacherSchedules",
                type: "uuid",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSchedules_StudentId",
                table: "TeacherSchedules",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSchedules_Students_StudentId",
                table: "TeacherSchedules",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSchedules_Students_StudentId",
                table: "TeacherSchedules");

            migrationBuilder.DropIndex(
                name: "IX_TeacherSchedules_StudentId",
                table: "TeacherSchedules");

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

            migrationBuilder.DropColumn(
                name: "ClassStatusses",
                table: "TeacherSchedules");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "TeacherSchedules");

            migrationBuilder.AddColumn<bool>(
                name: "IsBooked",
                table: "TeacherSchedules",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ClassBookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassBookings_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassBookings_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ClassBookings_StudentId",
                table: "ClassBookings",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassBookings_TeacherId",
                table: "ClassBookings",
                column: "TeacherId");
        }
    }
}
