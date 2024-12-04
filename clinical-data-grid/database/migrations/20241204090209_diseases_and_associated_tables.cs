using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace clinical_data_grid.database.migrations
{
    /// <inheritdoc />
    public partial class diseases_and_associated_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "diseases",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diseases", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "prescription_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prescription_template", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "symptoms",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_symptoms", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "symptoms_medicine_join",
                columns: table => new
                {
                    DiseasesId = table.Column<int>(type: "integer", nullable: false),
                    MedicinesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_symptoms_medicine_join", x => new { x.DiseasesId, x.MedicinesId });
                    table.ForeignKey(
                        name: "FK_symptoms_medicine_join_diseases_DiseasesId",
                        column: x => x.DiseasesId,
                        principalTable: "diseases",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_symptoms_medicine_join_medicines_MedicinesId",
                        column: x => x.MedicinesId,
                        principalTable: "medicines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "symptoms_diseases_join",
                columns: table => new
                {
                    DiseasesId = table.Column<int>(type: "integer", nullable: false),
                    SymptomsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_symptoms_diseases_join", x => new { x.DiseasesId, x.SymptomsId });
                    table.ForeignKey(
                        name: "FK_symptoms_diseases_join_diseases_DiseasesId",
                        column: x => x.DiseasesId,
                        principalTable: "diseases",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_symptoms_diseases_join_symptoms_SymptomsId",
                        column: x => x.SymptomsId,
                        principalTable: "symptoms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_symptoms_diseases_join_SymptomsId",
                table: "symptoms_diseases_join",
                column: "SymptomsId");

            migrationBuilder.CreateIndex(
                name: "IX_symptoms_medicine_join_MedicinesId",
                table: "symptoms_medicine_join",
                column: "MedicinesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "prescription_template");

            migrationBuilder.DropTable(
                name: "symptoms_diseases_join");

            migrationBuilder.DropTable(
                name: "symptoms_medicine_join");

            migrationBuilder.DropTable(
                name: "symptoms");

            migrationBuilder.DropTable(
                name: "diseases");
        }
    }
}
