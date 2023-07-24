using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Portal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EmploymentHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contract",
                table: "EmploymentHistory");

            migrationBuilder.DropColumn(
                name: "Resume",
                table: "EmploymentHistory");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "FormerPosition",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "FormerPosition",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "FormerPosition",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContractId",
                table: "EmploymentHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResumeId",
                table: "EmploymentHistory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeFile", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentHistory_ContractId",
                table: "EmploymentHistory",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentHistory_ResumeId",
                table: "EmploymentHistory",
                column: "ResumeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmploymentHistory_EmployeeFile_ContractId",
                table: "EmploymentHistory",
                column: "ContractId",
                principalTable: "EmployeeFile",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmploymentHistory_EmployeeFile_ResumeId",
                table: "EmploymentHistory",
                column: "ResumeId",
                principalTable: "EmployeeFile",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmploymentHistory_EmployeeFile_ContractId",
                table: "EmploymentHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_EmploymentHistory_EmployeeFile_ResumeId",
                table: "EmploymentHistory");

            migrationBuilder.DropTable(
                name: "EmployeeFile");

            migrationBuilder.DropIndex(
                name: "IX_EmploymentHistory_ContractId",
                table: "EmploymentHistory");

            migrationBuilder.DropIndex(
                name: "IX_EmploymentHistory_ResumeId",
                table: "EmploymentHistory");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "EmploymentHistory");

            migrationBuilder.DropColumn(
                name: "ResumeId",
                table: "EmploymentHistory");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "FormerPosition",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "FormerPosition",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "FormerPosition",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Contract",
                table: "EmploymentHistory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Resume",
                table: "EmploymentHistory",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
