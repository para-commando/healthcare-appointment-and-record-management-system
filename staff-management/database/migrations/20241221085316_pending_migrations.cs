using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace staffmanagement.database.migrations
{
    /// <inheritdoc />
    public partial class pending_migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "doctor_registration_date",
                table: "doctor-details",
                newName: "doctor_date_of_joining");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "doctor_date_of_joining",
                table: "doctor-details",
                newName: "doctor_registration_date");
        }
    }
}
