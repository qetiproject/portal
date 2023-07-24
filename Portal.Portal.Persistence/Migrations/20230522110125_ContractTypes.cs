using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Portal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ContractTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupervisorId",
                table: "EmploymentHistory");

            migrationBuilder.AddColumn<string>(
                name: "ContractType",
                table: "EmploymentHistory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Supervisor",
                table: "EmploymentHistory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContractTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTypes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractTypes");

            migrationBuilder.DropColumn(
                name: "ContractType",
                table: "EmploymentHistory");

            migrationBuilder.DropColumn(
                name: "Supervisor",
                table: "EmploymentHistory");

            migrationBuilder.AddColumn<int>(
                name: "SupervisorId",
                table: "EmploymentHistory",
                type: "int",
                nullable: true);
        }
    }
}
