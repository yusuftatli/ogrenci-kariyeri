using Microsoft.EntityFrameworkCore.Migrations;

namespace SCA.DataAccess.Migrations
{
    public partial class testw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_EducationStatus_EducationStatusId",
                schema: "public",
                table: "Users");

            migrationBuilder.AlterColumn<long>(
                name: "EducationStatusId",
                schema: "public",
                table: "Users",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Users_EducationStatus_EducationStatusId",
                schema: "public",
                table: "Users",
                column: "EducationStatusId",
                principalTable: "EducationStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_EducationStatus_EducationStatusId",
                schema: "public",
                table: "Users");

            migrationBuilder.AlterColumn<long>(
                name: "EducationStatusId",
                schema: "public",
                table: "Users",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_EducationStatus_EducationStatusId",
                schema: "public",
                table: "Users",
                column: "EducationStatusId",
                principalTable: "EducationStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
