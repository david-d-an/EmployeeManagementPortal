using Microsoft.EntityFrameworkCore.Migrations;

namespace EMP.DbScaffold.Migrations.Employees
{
    public partial class UpdatedDb_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "departments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "departments");
        }
    }
}
