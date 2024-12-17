using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace prescription_management.database.migrations
{
    /// <inheritdoc />
    public partial class pending_changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "appointment_details",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    doctor_id = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    appointment_date = table.Column<DateOnly>(type: "DATE", nullable: false),
                    appointment_time_of_day = table.Column<string>(type: "text", nullable: false),
                    appointment_booked_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    appointment_status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointment_details", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointment_details");
        }
    }
}
