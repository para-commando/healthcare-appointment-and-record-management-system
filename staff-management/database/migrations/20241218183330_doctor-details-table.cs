using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace staffmanagement.database.migrations
{
    /// <inheritdoc />
    public partial class doctordetailstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "doctor-details",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    doctor_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    doctor_specialization = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    doctor_address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    doctor_contact = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    doctor_unique_id = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    doctor_registration_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doctor-details", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "doctor-details");
        }
    }
}
