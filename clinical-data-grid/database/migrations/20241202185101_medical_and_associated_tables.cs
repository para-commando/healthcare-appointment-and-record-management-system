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
                name: "MedicineBenefits",
                columns: table => new
                {
                    MedicineId = table.Column<int>(type: "integer", nullable: false),
                    BenefitId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineBenefits", x => new { x.MedicineId, x.BenefitId });
                    table.ForeignKey(
                        name: "FK_MedicineBenefits_medicine_benefits_BenefitId",
                        column: x => x.BenefitId,
                        principalTable: "medicine_benefits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicineBenefits_medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "medicines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicineSideEffects",
                columns: table => new
                {
                    MedicineId = table.Column<int>(type: "integer", nullable: false),
                    SideEffectId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineSideEffects", x => new { x.MedicineId, x.SideEffectId });
                    table.ForeignKey(
                        name: "FK_MedicineSideEffects_medical_side_effects_SideEffectId",
                        column: x => x.SideEffectId,
                        principalTable: "medical_side_effects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicineSideEffects_medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "medicines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicineBenefits_BenefitId",
                table: "MedicineBenefits",
                column: "BenefitId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineSideEffects_SideEffectId",
                table: "MedicineSideEffects",
                column: "SideEffectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicineBenefits");

            migrationBuilder.DropTable(
                name: "MedicineSideEffects");

            migrationBuilder.DropTable(
                name: "medicine_benefits");

            migrationBuilder.DropTable(
                name: "medical_side_effects");

            migrationBuilder.DropTable(
                name: "medicines");
        }
    }
}
