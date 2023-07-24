using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Portal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NonComplianceViolator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ViolatorComment",
                table: "NonCompliances",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ViolatorFileId",
                table: "NonCompliances",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NonCompliances_ViolatorFileId",
                table: "NonCompliances",
                column: "ViolatorFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_NonCompliances_NonComplianceFile_ViolatorFileId",
                table: "NonCompliances",
                column: "ViolatorFileId",
                principalTable: "NonComplianceFile",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NonCompliances_NonComplianceFile_ViolatorFileId",
                table: "NonCompliances");

            migrationBuilder.DropIndex(
                name: "IX_NonCompliances_ViolatorFileId",
                table: "NonCompliances");

            migrationBuilder.DropColumn(
                name: "ViolatorComment",
                table: "NonCompliances");

            migrationBuilder.DropColumn(
                name: "ViolatorFileId",
                table: "NonCompliances");
        }
    }
}
