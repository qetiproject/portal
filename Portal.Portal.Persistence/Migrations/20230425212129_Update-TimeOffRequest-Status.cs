using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Portal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTimeOffRequestStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StatusChanger",
                table: "TimeOffRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StatusComment",
                table: "TimeOffRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusFileId",
                table: "TimeOffRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeOffRequests_StatusFileId",
                table: "TimeOffRequests",
                column: "StatusFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeOffRequests_TimeOffRequestFile_StatusFileId",
                table: "TimeOffRequests",
                column: "StatusFileId",
                principalTable: "TimeOffRequestFile",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeOffRequests_TimeOffRequestFile_StatusFileId",
                table: "TimeOffRequests");

            migrationBuilder.DropIndex(
                name: "IX_TimeOffRequests_StatusFileId",
                table: "TimeOffRequests");

            migrationBuilder.DropColumn(
                name: "StatusChanger",
                table: "TimeOffRequests");

            migrationBuilder.DropColumn(
                name: "StatusComment",
                table: "TimeOffRequests");

            migrationBuilder.DropColumn(
                name: "StatusFileId",
                table: "TimeOffRequests");
        }
    }
}
