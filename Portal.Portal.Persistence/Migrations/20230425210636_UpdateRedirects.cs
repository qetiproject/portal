using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Portal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRedirects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "Redirect");

            migrationBuilder.RenameColumn(
                name: "Receiver",
                table: "Redirect",
                newName: "Participant");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Redirect",
                newName: "Comment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Participant",
                table: "Redirect",
                newName: "Receiver");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "Redirect",
                newName: "Description");

            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                table: "Redirect",
                type: "datetime2",
                nullable: true);
        }
    }
}
