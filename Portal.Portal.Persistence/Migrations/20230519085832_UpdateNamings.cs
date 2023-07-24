using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Portal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNamings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "JobInfo");

            migrationBuilder.RenameColumn(
                name: "To",
                table: "TimeOffRequests",
                newName: "Including");

            migrationBuilder.RenameColumn(
                name: "Retirement",
                table: "PayrollBasic",
                newName: "Residency");

            migrationBuilder.RenameColumn(
                name: "PhysicalPerson",
                table: "PayrollBasic",
                newName: "ParticipationInPension");

            migrationBuilder.RenameColumn(
                name: "EmployeeAccess",
                table: "EmploymentHistory",
                newName: "EmployeeRole");

            migrationBuilder.RenameColumn(
                name: "ContractTerm",
                table: "EmploymentHistory",
                newName: "ContractExpirationDate");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeNumber",
                table: "PersonalInformation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdDocumentId",
                table: "JobInfo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupervisorId",
                table: "EmploymentHistory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobInfo_IdDocumentId",
                table: "JobInfo",
                column: "IdDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobInfo_EmployeeFile_IdDocumentId",
                table: "JobInfo",
                column: "IdDocumentId",
                principalTable: "EmployeeFile",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobInfo_EmployeeFile_IdDocumentId",
                table: "JobInfo");

            migrationBuilder.DropIndex(
                name: "IX_JobInfo_IdDocumentId",
                table: "JobInfo");

            migrationBuilder.DropColumn(
                name: "EmployeeNumber",
                table: "PersonalInformation");

            migrationBuilder.DropColumn(
                name: "IdDocumentId",
                table: "JobInfo");

            migrationBuilder.DropColumn(
                name: "SupervisorId",
                table: "EmploymentHistory");

            migrationBuilder.RenameColumn(
                name: "Including",
                table: "TimeOffRequests",
                newName: "To");

            migrationBuilder.RenameColumn(
                name: "Residency",
                table: "PayrollBasic",
                newName: "Retirement");

            migrationBuilder.RenameColumn(
                name: "ParticipationInPension",
                table: "PayrollBasic",
                newName: "PhysicalPerson");

            migrationBuilder.RenameColumn(
                name: "EmployeeRole",
                table: "EmploymentHistory",
                newName: "EmployeeAccess");

            migrationBuilder.RenameColumn(
                name: "ContractExpirationDate",
                table: "EmploymentHistory",
                newName: "ContractTerm");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "JobInfo",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
