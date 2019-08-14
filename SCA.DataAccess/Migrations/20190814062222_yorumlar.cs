using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SCA.DataAccess.Migrations
{
    public partial class yorumlar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UsersId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UsersId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Comments",
                newName: "userName");

            migrationBuilder.AddColumn<string>(
                name: "DescriYorumption",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PostDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriYorumption",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "PostDate",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "userName",
                table: "Comments",
                newName: "Description");

            migrationBuilder.AddColumn<long>(
                name: "UsersId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UsersId",
                table: "Comments",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UsersId",
                table: "Comments",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
