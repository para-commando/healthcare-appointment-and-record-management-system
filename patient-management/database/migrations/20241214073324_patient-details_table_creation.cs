using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace prescription_management.database.migrations
{
    /// <inheritdoc />
    public partial class patientdetails_table_creation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "patient-details",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    patient_address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    patient_contact = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    patient_unique_id = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    patient_registration_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    patient_latest_date_of_visit = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patient-details", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "patient-details");
        }
    }
}
