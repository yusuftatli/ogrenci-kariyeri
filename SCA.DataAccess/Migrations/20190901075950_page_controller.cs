using Microsoft.EntityFrameworkCore.Migrations;

namespace SCA.DataAccess.Migrations
{
    public partial class page_controller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "EducationStatusId",
                schema: "public",
                table: "Users",
                nullable: false,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "EducationStatusId",
                schema: "public",
                table: "Users",
                nullable: false,
                oldClrType: typeof(byte));
        }
    }
}
