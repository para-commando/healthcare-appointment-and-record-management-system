using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prescription_management.database.migrations
{
    /// <inheritdoc />
    public partial class seed_patientdetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
     migrationBuilder.InsertData(
                table: "patient-details",
                columns: new[] { "id", "patient_name", "patient_address", "patient_contact", "patient_unique_id", "patient_registration_date", "patient_latest_date_of_visit" },
                values: new object[,]
                {
                    { 1, "John Doe", "789 Oak St", "5551234567", "P001", new DateOnly(2023, 6, 1), new DateOnly(2024, 5, 30) },
                    { 2, "Jane Smith", "321 Pine St", "5559876543", "P002", new DateOnly(2023, 7, 15), new DateOnly(2024, 5, 28) }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
      migrationBuilder.DeleteData(
                table: "patient-details",
                keyColumn: "id",
                keyValues: new object[] { 1, 2 }
            );
        }
    }
}
