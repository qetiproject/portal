using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Portal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NonCompliances : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NonComplianceFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonComplianceFile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NonCompliances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Violator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fine = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StatusChanger = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusFileId = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileId = table.Column<int>(type: "int", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonCompliances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NonCompliances_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NonCompliances_NonComplianceFile_FileId",
                        column: x => x.FileId,
                        principalTable: "NonComplianceFile",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NonCompliances_NonComplianceFile_StatusFileId",
                        column: x => x.StatusFileId,
                        principalTable: "NonComplianceFile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NonComplianceRedirect",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Redirecter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Participant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RightOfConfirmation = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileId = table.Column<int>(type: "int", nullable: true),
                    NonComplianceId = table.Column<int>(type: "int", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonComplianceRedirect", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NonComplianceRedirect_NonComplianceFile_FileId",
                        column: x => x.FileId,
                        principalTable: "NonComplianceFile",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NonComplianceRedirect_NonCompliances_NonComplianceId",
                        column: x => x.NonComplianceId,
                        principalTable: "NonCompliances",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NonComplianceRedirect_FileId",
                table: "NonComplianceRedirect",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_NonComplianceRedirect_NonComplianceId",
                table: "NonComplianceRedirect",
                column: "NonComplianceId");

            migrationBuilder.CreateIndex(
                name: "IX_NonCompliances_FileId",
                table: "NonCompliances",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_NonCompliances_GroupId",
                table: "NonCompliances",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_NonCompliances_StatusFileId",
                table: "NonCompliances",
                column: "StatusFileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NonComplianceRedirect");

            migrationBuilder.DropTable(
                name: "NonCompliances");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "NonComplianceFile");
        }
    }
}
