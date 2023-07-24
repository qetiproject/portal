using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Portal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTimeOffRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Redirect_TimeOffRequestFile_FileId",
                table: "Redirect");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeOffRequests_TimeOffRequestFile_FileId",
                table: "TimeOffRequests");

            migrationBuilder.AlterColumn<int>(
                name: "FileId",
                table: "TimeOffRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TimeOffRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "TimeOffRequests",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "RightOfConfirmation",
                table: "Redirect",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "FileId",
                table: "Redirect",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Redirect",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "Redirect",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_Redirect_TimeOffRequestFile_FileId",
                table: "Redirect",
                column: "FileId",
                principalTable: "TimeOffRequestFile",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeOffRequests_TimeOffRequestFile_FileId",
                table: "TimeOffRequests",
                column: "FileId",
                principalTable: "TimeOffRequestFile",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Redirect_TimeOffRequestFile_FileId",
                table: "Redirect");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeOffRequests_TimeOffRequestFile_FileId",
                table: "TimeOffRequests");

            migrationBuilder.AlterColumn<int>(
                name: "FileId",
                table: "TimeOffRequests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TimeOffRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "TimeOffRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "RightOfConfirmation",
                table: "Redirect",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FileId",
                table: "Redirect",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Redirect",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "Redirect",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Redirect_TimeOffRequestFile_FileId",
                table: "Redirect",
                column: "FileId",
                principalTable: "TimeOffRequestFile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeOffRequests_TimeOffRequestFile_FileId",
                table: "TimeOffRequests",
                column: "FileId",
                principalTable: "TimeOffRequestFile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
