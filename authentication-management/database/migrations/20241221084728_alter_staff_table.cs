using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace authenticationmanagement.database.migrations
{
    /// <inheritdoc />
    public partial class alter_staff_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "specialization",
                table: "staffs",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "specialization",
                table: "staffs");
        }
    }
}
