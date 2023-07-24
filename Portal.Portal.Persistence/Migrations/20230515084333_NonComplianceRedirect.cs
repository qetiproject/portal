using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Portal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NonComplianceRedirect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RightOfConfirmation",
                table: "NonComplianceRedirect",
                newName: "StatusChange");

            migrationBuilder.RenameColumn(
                name: "Participant",
                table: "NonComplianceRedirect",
                newName: "Receiver");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StatusChange",
                table: "NonComplianceRedirect",
                newName: "RightOfConfirmation");

            migrationBuilder.RenameColumn(
                name: "Receiver",
                table: "NonComplianceRedirect",
                newName: "Participant");
        }
    }
}
