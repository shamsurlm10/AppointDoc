using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppointDoc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initializingdatabasev1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "doctors",
                columns: table => new
                {
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doctors", x => x.DoctorId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PatientContactInformation = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    AppointmentDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_appointments_doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "doctors",
                columns: new[] { "DoctorId", "DoctorName" },
                values: new object[,]
                {
                    { new Guid("55818433-015a-4bcf-9f88-db29e45b85a2"), "Dr. Mawa" },
                    { new Guid("61739faf-af55-4371-8298-364c325e69cc"), "Dr. Ashiq" },
                    { new Guid("9b4afc8b-6aad-4623-bc90-08a072523c57"), "Dr. Alam" },
                    { new Guid("ab012994-b8c5-4b40-b9d0-98d12d1be579"), "Dr. Shamsur" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_appointments_DoctorId",
                table: "appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_users_Username",
                table: "users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointments");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "doctors");
        }
    }
}
