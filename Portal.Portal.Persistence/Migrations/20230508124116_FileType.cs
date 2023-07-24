using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Portal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FileType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "TimeOffRequestFile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "EmployeeFile",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "TimeOffRequestFile");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "EmployeeFile");
        }
    }
}
