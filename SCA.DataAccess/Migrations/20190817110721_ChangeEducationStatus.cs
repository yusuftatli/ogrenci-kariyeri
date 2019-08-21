using Microsoft.EntityFrameworkCore.Migrations;

namespace SCA.DataAccess.Migrations
{
    public partial class ChangeEducationStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_EducationStatus_EducationStatusId",
                schema: "public",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_EducationStatusId",
                schema: "public",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "EducationStatusId",
                schema: "public",
                table: "Users",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EducationStatusId1",
                schema: "public",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                schema: "public",
                table: "Users",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_EducationStatusId1",
                schema: "public",
                table: "Users",
                column: "EducationStatusId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_EducationStatus_EducationStatusId1",
                schema: "public",
                table: "Users",
                column: "EducationStatusId1",
                principalTable: "EducationStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_EducationStatus_EducationStatusId1",
                schema: "public",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_EducationStatusId1",
                schema: "public",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EducationStatusId1",
                schema: "public",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserName",
                schema: "public",
                table: "Users");

            migrationBuilder.AlterColumn<long>(
                name: "EducationStatusId",
                schema: "public",
                table: "Users",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_EducationStatusId",
                schema: "public",
                table: "Users",
                column: "EducationStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_EducationStatus_EducationStatusId",
                schema: "public",
                table: "Users",
                column: "EducationStatusId",
                principalTable: "EducationStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
