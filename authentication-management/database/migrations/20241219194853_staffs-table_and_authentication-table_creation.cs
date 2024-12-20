using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace authenticationmanagement.database.migrations
{
    /// <inheritdoc />
    public partial class staffstable_and_authenticationtable_creation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "doctor-details");

            migrationBuilder.CreateTable(
                name: "authentication_creds",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_name = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    roles = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authentication_creds", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "staffs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    staff_unique_id = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    unique_id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    contact = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    date_of_joining = table.Column<DateOnly>(type: "date", nullable: false),
                    designation = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staffs", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "authentication_creds");

            migrationBuilder.DropTable(
                name: "staffs");

            migrationBuilder.CreateTable(
                name: "doctor-details",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    doctor_address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    doctor_contact = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    doctor_registration_date = table.Column<DateOnly>(type: "date", nullable: false),
                    doctor_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    doctor_specialization = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    doctor_unique_id = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doctor-details", x => x.id);
                });
        }
    }
}
