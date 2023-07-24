using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Portal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmployeeUniversity3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate3",
                table: "EmployeeUniversity");

            migrationBuilder.DropColumn(
                name: "StartDate3",
                table: "EmployeeUniversity");

            migrationBuilder.RenameColumn(
                name: "StartDate1",
                table: "EmployeeUniversity",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "EndDate2",
                table: "EmployeeUniversity",
                newName: "EndDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "EmployeeUniversity",
                newName: "StartDate1");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "EmployeeUniversity",
                newName: "EndDate2");

            migrationBuilder.AddColumn<int>(
                name: "EndDate3",
                table: "EmployeeUniversity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartDate3",
                table: "EmployeeUniversity",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
