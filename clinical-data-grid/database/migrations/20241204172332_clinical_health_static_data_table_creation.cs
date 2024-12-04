using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace clinical_data_grid.database.migrations
{
    /// <inheritdoc />
    public partial class clinical_health_static_data_table_creation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clinical_health_static_data",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    disease = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    composition = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    medicine_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    uses = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    side_effects = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clinical_health_static_data", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clinical_health_static_data");
        }
    }
}
