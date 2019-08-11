using Microsoft.EntityFrameworkCore.Migrations;

namespace SCA.DataAccess.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Content",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Category",
                table: "Content",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
