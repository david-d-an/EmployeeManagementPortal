using Microsoft.EntityFrameworkCore.Migrations;

namespace EMP.DbScaffold.Migrations.Employees
{
    public partial class UpdatedDb_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Zip",
                table: "departments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Zip",
                table: "departments");
        }
    }
}
