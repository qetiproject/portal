using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Portal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PhotoHidden : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Hidden",
                table: "EmployeePhoto",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hidden",
                table: "EmployeePhoto");
        }
    }
}
