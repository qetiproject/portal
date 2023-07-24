using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Portal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmployeeUniversity1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "EmployeeUniversity",
                newName: "StartDate3");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "EmployeeUniversity",
                newName: "EndDate3");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate3",
                table: "EmployeeUniversity",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "EndDate3",
                table: "EmployeeUniversity",
                newName: "EndDate");
        }
    }
}
