using Microsoft.EntityFrameworkCore.Migrations;

namespace EMP.DbScaffold.Migrations.Employees
{
    public partial class UpdatedDb_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeptShortName",
                table: "departments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeptShortName",
                table: "departments");
        }
    }
}
