using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace clinical_data_grid.database.migrations
{
    /// <inheritdoc />
    public partial class medical_and_associated_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "medical_side_effects",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    side_effects = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medical_side_effects", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "medicine_benefits",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    benefit = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicine_benefits", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "medicines",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    medicine_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    medicine_details = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicines", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "medicine_benefits_join",
                columns: table => new
                {
                    MedicineId = table.Column<int>(type: "integer", nullable: false),
                    BenefitId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicine_benefits_join", x => new { x.MedicineId, x.BenefitId });
                    table.ForeignKey(
                        name: "FK_medicine_benefits_join_medicine_benefits_BenefitId",
                        column: x => x.BenefitId,
                        principalTable: "medicine_benefits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_medicine_benefits_join_medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "medicines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "medicine_sideEffects_join",
                columns: table => new
                {
                    MedicineId = table.Column<int>(type: "integer", nullable: false),
                    SideEffectId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicine_sideEffects_join", x => new { x.MedicineId, x.SideEffectId });
                    table.ForeignKey(
                        name: "FK_medicine_sideEffects_join_medical_side_effects_SideEffectId",
                        column: x => x.SideEffectId,
                        principalTable: "medical_side_effects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_medicine_sideEffects_join_medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "medicines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_medicine_benefits_join_BenefitId",
                table: "medicine_benefits_join",
                column: "BenefitId");

            migrationBuilder.CreateIndex(
                name: "IX_medicine_sideEffects_join_SideEffectId",
                table: "medicine_sideEffects_join",
                column: "SideEffectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "medicine_benefits_join");

            migrationBuilder.DropTable(
                name: "medicine_sideEffects_join");

            migrationBuilder.DropTable(
                name: "medicine_benefits");

            migrationBuilder.DropTable(
                name: "medical_side_effects");

            migrationBuilder.DropTable(
                name: "medicines");
        }
    }
}
