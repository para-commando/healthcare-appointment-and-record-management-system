using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prescription_management.database.migrations
{
    /// <inheritdoc />
    public partial class changingcolofdocandpatientid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "patient_id",
                table: "appointment_details");

            migrationBuilder.DropColumn(
                name: "doctor_id",
                table: "appointment_details");

            migrationBuilder.AddColumn<int>(
                name: "doctor_id",
                table: "appointment_details",
                nullable: false
                );

            migrationBuilder.AddColumn<int>(
            name: "patient_id",
            table: "appointment_details",
            nullable: false
           );
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "patient_id",
                table: "appointment_details");

            migrationBuilder.DropColumn(
                name: "doctor_id",
                table: "appointment_details");

            migrationBuilder.AddColumn<string>(
                name: "doctor_id",
                table: "appointment_details",
                nullable: false
               );

            migrationBuilder.AddColumn<string>(
            name: "patient_id",
            table: "appointment_details",
            nullable: false
            );
        }
    }
}
