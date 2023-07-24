using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Portal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeRoleId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeRole",
                table: "EmploymentHistory");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeRoleId",
                table: "EmploymentHistory",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeRoleId",
                table: "EmploymentHistory");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeRole",
                table: "EmploymentHistory",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
