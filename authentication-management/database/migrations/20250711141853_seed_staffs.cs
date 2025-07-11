using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace authenticationmanagement.database.migrations
{
    /// <inheritdoc />
    public partial class seed_staffs : Migration
    {
      protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Seed sample data for staffs
            migrationBuilder.InsertData(
                table: "staffs",
                columns: new[] { "id", "staff_unique_id", "email", "date_of_birth", "address", "unique_id", "contact", "date_of_joining", "designation", "specialization" },
                values: new object[,]
                {
                    { 1, "S001", "staff1@example.com", new DateOnly(1985, 5, 20), "123 Main St", "U001", "1234567890", new DateOnly(2020, 1, 15), "Doctor", "Cardiology" },
                    { 2, "S002", "staff2@example.com", new DateOnly(1990, 8, 10), "456 Elm St", "U002", "0987654321", new DateOnly(2021, 3, 10), "Nurse", "Pediatrics" }
                }
            );

      
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "staffs",
                keyColumn: "id",
                keyValues: new object[] { 1, 2 }
            );

          
        }
    }
}
