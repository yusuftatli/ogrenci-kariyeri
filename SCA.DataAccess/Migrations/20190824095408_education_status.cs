using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SCA.DataAccess.Migrations
{
    public partial class education_status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyClubs_Sector_SectorId",
                table: "CompanyClubs");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_EducationStatus_EducationStatusId1",
                schema: "public",
                table: "Users");

            migrationBuilder.DropTable(
                name: "EducationStatus");

            migrationBuilder.DropIndex(
                name: "IX_Users_EducationStatusId1",
                schema: "public",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_CompanyClubs_SectorId",
                table: "CompanyClubs");

            migrationBuilder.DropColumn(
                name: "EducationStatusId1",
                schema: "public",
                table: "Users");

            migrationBuilder.AlterColumn<long>(
                name: "EducationStatusId",
                schema: "public",
                table: "Users",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "EducationStatusId",
                schema: "public",
                table: "Users",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "EducationStatusId1",
                schema: "public",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EducationStatus",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_EducationStatusId1",
                schema: "public",
                table: "Users",
                column: "EducationStatusId1");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyClubs_SectorId",
                table: "CompanyClubs",
                column: "SectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyClubs_Sector_SectorId",
                table: "CompanyClubs",
                column: "SectorId",
                principalTable: "Sector",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_EducationStatus_EducationStatusId1",
                schema: "public",
                table: "Users",
                column: "EducationStatusId1",
                principalTable: "EducationStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
