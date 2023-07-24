using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Portal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NonCompliances1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NonCompliances_Groups_GroupId",
                table: "NonCompliances");

            migrationBuilder.DropIndex(
                name: "IX_NonCompliances_GroupId",
                table: "NonCompliances");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "NonCompliances");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "NonCompliances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_NonCompliances_GroupId",
                table: "NonCompliances",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_NonCompliances_Groups_GroupId",
                table: "NonCompliances",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
