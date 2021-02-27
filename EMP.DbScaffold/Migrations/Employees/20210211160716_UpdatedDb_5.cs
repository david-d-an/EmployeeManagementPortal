using Microsoft.EntityFrameworkCore.Migrations;

namespace EMP.DbScaffold.Migrations.Employees
{
    public partial class UpdatedDb_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "DeptShortName",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "State",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "Zip",
                table: "departments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "departments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeptShortName",
                table: "departments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "departments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zip",
                table: "departments",
                type: "text",
                nullable: true);
        }
    }
}
