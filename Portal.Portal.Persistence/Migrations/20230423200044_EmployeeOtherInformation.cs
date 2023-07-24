using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Portal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeOtherInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmergencyContact",
                table: "OtherInformation");

            migrationBuilder.DropColumn(
                name: "EmergencyContactNumber",
                table: "OtherInformation");

            migrationBuilder.DropColumn(
                name: "EmergencyContactRelationship",
                table: "OtherInformation");

            migrationBuilder.AddColumn<int>(
                name: "AlergiesId",
                table: "OtherInformation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BloodGroupAndRhesusId",
                table: "OtherInformation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DrivingLicenseId",
                table: "OtherInformation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicalCertificateId",
                table: "OtherInformation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmergencyContactId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmergencyContact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Relationship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyContact", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OtherInformation_AlergiesId",
                table: "OtherInformation",
                column: "AlergiesId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherInformation_BloodGroupAndRhesusId",
                table: "OtherInformation",
                column: "BloodGroupAndRhesusId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherInformation_DrivingLicenseId",
                table: "OtherInformation",
                column: "DrivingLicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherInformation_MedicalCertificateId",
                table: "OtherInformation",
                column: "MedicalCertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmergencyContactId",
                table: "Employees",
                column: "EmergencyContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmergencyContact_EmergencyContactId",
                table: "Employees",
                column: "EmergencyContactId",
                principalTable: "EmergencyContact",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherInformation_EmployeeFile_AlergiesId",
                table: "OtherInformation",
                column: "AlergiesId",
                principalTable: "EmployeeFile",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherInformation_EmployeeFile_BloodGroupAndRhesusId",
                table: "OtherInformation",
                column: "BloodGroupAndRhesusId",
                principalTable: "EmployeeFile",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherInformation_EmployeeFile_DrivingLicenseId",
                table: "OtherInformation",
                column: "DrivingLicenseId",
                principalTable: "EmployeeFile",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherInformation_EmployeeFile_MedicalCertificateId",
                table: "OtherInformation",
                column: "MedicalCertificateId",
                principalTable: "EmployeeFile",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmergencyContact_EmergencyContactId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherInformation_EmployeeFile_AlergiesId",
                table: "OtherInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherInformation_EmployeeFile_BloodGroupAndRhesusId",
                table: "OtherInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherInformation_EmployeeFile_DrivingLicenseId",
                table: "OtherInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherInformation_EmployeeFile_MedicalCertificateId",
                table: "OtherInformation");

            migrationBuilder.DropTable(
                name: "EmergencyContact");

            migrationBuilder.DropIndex(
                name: "IX_OtherInformation_AlergiesId",
                table: "OtherInformation");

            migrationBuilder.DropIndex(
                name: "IX_OtherInformation_BloodGroupAndRhesusId",
                table: "OtherInformation");

            migrationBuilder.DropIndex(
                name: "IX_OtherInformation_DrivingLicenseId",
                table: "OtherInformation");

            migrationBuilder.DropIndex(
                name: "IX_OtherInformation_MedicalCertificateId",
                table: "OtherInformation");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmergencyContactId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "AlergiesId",
                table: "OtherInformation");

            migrationBuilder.DropColumn(
                name: "BloodGroupAndRhesusId",
                table: "OtherInformation");

            migrationBuilder.DropColumn(
                name: "DrivingLicenseId",
                table: "OtherInformation");

            migrationBuilder.DropColumn(
                name: "MedicalCertificateId",
                table: "OtherInformation");

            migrationBuilder.DropColumn(
                name: "EmergencyContactId",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContact",
                table: "OtherInformation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactNumber",
                table: "OtherInformation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactRelationship",
                table: "OtherInformation",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
