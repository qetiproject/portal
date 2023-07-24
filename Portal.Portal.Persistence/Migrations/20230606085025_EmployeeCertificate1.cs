using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Portal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeCertificate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OtherInformation_EmployeeFile_CertificateId",
                table: "OtherInformation");

            migrationBuilder.DropIndex(
                name: "IX_OtherInformation_CertificateId",
                table: "OtherInformation");

            migrationBuilder.DropColumn(
                name: "CertificateId",
                table: "OtherInformation");

            migrationBuilder.AddColumn<int>(
                name: "CertificateId",
                table: "EmployeeUniversity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CertificateId",
                table: "EmployeeTraining",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeUniversity_CertificateId",
                table: "EmployeeUniversity",
                column: "CertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTraining_CertificateId",
                table: "EmployeeTraining",
                column: "CertificateId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTraining_EmployeeFile_CertificateId",
                table: "EmployeeTraining",
                column: "CertificateId",
                principalTable: "EmployeeFile",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeUniversity_EmployeeFile_CertificateId",
                table: "EmployeeUniversity",
                column: "CertificateId",
                principalTable: "EmployeeFile",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTraining_EmployeeFile_CertificateId",
                table: "EmployeeTraining");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeUniversity_EmployeeFile_CertificateId",
                table: "EmployeeUniversity");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeUniversity_CertificateId",
                table: "EmployeeUniversity");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeTraining_CertificateId",
                table: "EmployeeTraining");

            migrationBuilder.DropColumn(
                name: "CertificateId",
                table: "EmployeeUniversity");

            migrationBuilder.DropColumn(
                name: "CertificateId",
                table: "EmployeeTraining");

            migrationBuilder.AddColumn<int>(
                name: "CertificateId",
                table: "OtherInformation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OtherInformation_CertificateId",
                table: "OtherInformation",
                column: "CertificateId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherInformation_EmployeeFile_CertificateId",
                table: "OtherInformation",
                column: "CertificateId",
                principalTable: "EmployeeFile",
                principalColumn: "Id");
        }
    }
}
