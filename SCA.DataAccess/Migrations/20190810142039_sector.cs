using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SCA.DataAccess.Migrations
{
    public partial class sector : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CompanyClubsId",
                table: "SocialMedia",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CompanyClupId",
                table: "SocialMedia",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "SocialMedia",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UsersId",
                table: "SocialMedia",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompanyClubs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CompanyClupType = table.Column<byte>(nullable: false),
                    ShortName = table.Column<string>(nullable: true),
                    Sector = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyClubs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedia_CompanyClubsId",
                table: "SocialMedia",
                column: "CompanyClubsId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedia_UsersId",
                table: "SocialMedia",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_SocialMedia_CompanyClubs_CompanyClubsId",
                table: "SocialMedia",
                column: "CompanyClubsId",
                principalTable: "CompanyClubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SocialMedia_Users_UsersId",
                table: "SocialMedia",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialMedia_CompanyClubs_CompanyClubsId",
                table: "SocialMedia");

            migrationBuilder.DropForeignKey(
                name: "FK_SocialMedia_Users_UsersId",
                table: "SocialMedia");

            migrationBuilder.DropTable(
                name: "CompanyClubs");

            migrationBuilder.DropIndex(
                name: "IX_SocialMedia_CompanyClubsId",
                table: "SocialMedia");

            migrationBuilder.DropIndex(
                name: "IX_SocialMedia_UsersId",
                table: "SocialMedia");

            migrationBuilder.DropColumn(
                name: "CompanyClubsId",
                table: "SocialMedia");

            migrationBuilder.DropColumn(
                name: "CompanyClupId",
                table: "SocialMedia");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SocialMedia");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "SocialMedia");
        }
    }
}
