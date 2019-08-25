using Microsoft.EntityFrameworkCore.Migrations;

namespace SCA.DataAccess.Migrations
{
    public partial class sector2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "SectorId",
                table: "CompanyClubs",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "SectorTypeId",
                table: "CompanyClubs",
                nullable: false,
                defaultValue: 0L);

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
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyClubs_Sector_SectorId",
                table: "CompanyClubs");

            migrationBuilder.DropIndex(
                name: "IX_CompanyClubs_SectorId",
                table: "CompanyClubs");

            migrationBuilder.DropColumn(
                name: "SectorTypeId",
                table: "CompanyClubs");

            migrationBuilder.AlterColumn<long>(
                name: "SectorId",
                table: "CompanyClubs",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
