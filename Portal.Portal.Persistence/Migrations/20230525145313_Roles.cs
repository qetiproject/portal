using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Portal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permission_PermissionGroups_PermissionGroupId",
                table: "Permission");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_AspNetRoles_RoleId",
                table: "RolePermission");

            migrationBuilder.DropColumn(
                name: "ApplicationRoleId",
                table: "RolePermission");

            migrationBuilder.RenameColumn(
                name: "GroupName",
                table: "PermissionGroups",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "RolePermission",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeStamp",
                table: "RolePermission",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeStamp",
                table: "PermissionGroups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "PermissionGroupId",
                table: "Permission",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeStamp",
                table: "Permission",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleUser_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_RoleId",
                table: "RoleUser",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permission_PermissionGroups_PermissionGroupId",
                table: "Permission",
                column: "PermissionGroupId",
                principalTable: "PermissionGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_AspNetRoles_RoleId",
                table: "RolePermission",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permission_PermissionGroups_PermissionGroupId",
                table: "Permission");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_AspNetRoles_RoleId",
                table: "RolePermission");

            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "RolePermission");

            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "PermissionGroups");

            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Permission");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PermissionGroups",
                newName: "GroupName");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "RolePermission",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationRoleId",
                table: "RolePermission",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PermissionGroupId",
                table: "Permission",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Permission_PermissionGroups_PermissionGroupId",
                table: "Permission",
                column: "PermissionGroupId",
                principalTable: "PermissionGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_AspNetRoles_RoleId",
                table: "RolePermission",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
