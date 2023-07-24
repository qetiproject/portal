using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Portal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeCertificate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
