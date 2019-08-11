using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SCA.DataAccess.Migrations
{
    public partial class test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sector",
                table: "CompanyClubs",
                newName: "WebSite");

            migrationBuilder.AddColumn<int>(
                name: "PlatformType",
                table: "Content",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CompanyClubs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "CompanyClubs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeaderImage",
                table: "CompanyClubs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "CompanyClubs",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SectorId",
                table: "CompanyClubs",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<byte>(
                name: "SectorType",
                table: "CompanyClubs",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "CompanyClubs",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UsersId",
                table: "CompanyClubs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Anouncement",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_CompanyClubs_SectorId",
                table: "CompanyClubs",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyClubs_UsersId",
                table: "CompanyClubs",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyClubs_Sector_SectorId",
                table: "CompanyClubs",
                column: "SectorId",
                principalTable: "Sector",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyClubs_Users_UsersId",
                table: "CompanyClubs",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyClubs_Sector_SectorId",
                table: "CompanyClubs");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyClubs_Users_UsersId",
                table: "CompanyClubs");

            migrationBuilder.DropIndex(
                name: "IX_CompanyClubs_SectorId",
                table: "CompanyClubs");

            migrationBuilder.DropIndex(
                name: "IX_CompanyClubs_UsersId",
                table: "CompanyClubs");

            migrationBuilder.DropColumn(
                name: "PlatformType",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CompanyClubs");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "CompanyClubs");

            migrationBuilder.DropColumn(
                name: "HeaderImage",
                table: "CompanyClubs");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "CompanyClubs");

            migrationBuilder.DropColumn(
                name: "SectorId",
                table: "CompanyClubs");

            migrationBuilder.DropColumn(
                name: "SectorType",
                table: "CompanyClubs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CompanyClubs");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "CompanyClubs");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Anouncement");

            migrationBuilder.RenameColumn(
                name: "WebSite",
                table: "CompanyClubs",
                newName: "Sector");
        }
    }
}
