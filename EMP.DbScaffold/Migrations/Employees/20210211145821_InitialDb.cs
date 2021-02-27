using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace EMP.DbScaffold.Migrations.Employees
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.CreateTable(
            //     name: "__efmigrationshistory",
            //     columns: table => new
            //     {
            //         MigrationId = table.Column<string>(maxLength: 150, nullable: false),
            //         ProductVersion = table.Column<string>(maxLength: 32, nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PRIMARY", x => x.MigrationId);
            //     });

            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    dept_no = table.Column<string>(fixedLength: true, maxLength: 4, nullable: false),
                    dept_name = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.dept_no);
                });

            migrationBuilder.CreateTable(
                name: "dept_emp_current",
                columns: table => new
                {
                    emp_no = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    dept_no = table.Column<string>(fixedLength: true, maxLength: 4, nullable: false),
                    from_date = table.Column<DateTime>(type: "date", nullable: false),
                    to_date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.emp_no);
                });

            migrationBuilder.CreateTable(
                name: "dept_manager_current",
                columns: table => new
                {
                    dept_no = table.Column<string>(fixedLength: true, maxLength: 4, nullable: false),
                    emp_no = table.Column<int>(type: "int(11)", nullable: false),
                    from_date = table.Column<DateTime>(type: "date", nullable: false),
                    to_date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.dept_no);
                });

            migrationBuilder.CreateTable(
                name: "emp_details_cache",
                columns: table => new
                {
                    emp_no = table.Column<int>(type: "int(11)", nullable: false),
                    first_name = table.Column<string>(maxLength: 14, nullable: false),
                    last_name = table.Column<string>(maxLength: 16, nullable: false),
                    birth_date = table.Column<DateTime>(type: "date", nullable: false),
                    hire_date = table.Column<DateTime>(type: "date", nullable: false),
                    gender = table.Column<string>(type: "enum('M','F')", nullable: false),
                    salary = table.Column<int>(type: "int(11)", nullable: true),
                    title = table.Column<string>(maxLength: 50, nullable: true),
                    dept_no = table.Column<string>(fixedLength: true, maxLength: 4, nullable: false),
                    dept_name = table.Column<string>(maxLength: 40, nullable: false),
                    manager_first_name = table.Column<string>(maxLength: 14, nullable: false),
                    manager_last_name = table.Column<string>(maxLength: 16, nullable: false),
                    manager_emp_no = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    emp_no = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    birth_date = table.Column<DateTime>(type: "date", nullable: false),
                    first_name = table.Column<string>(maxLength: 14, nullable: false),
                    last_name = table.Column<string>(maxLength: 16, nullable: false),
                    gender = table.Column<string>(type: "enum('M','F')", nullable: false),
                    hire_date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.emp_no);
                });

            migrationBuilder.CreateTable(
                name: "salaries_current",
                columns: table => new
                {
                    emp_no = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    salary = table.Column<int>(type: "int(11)", nullable: false),
                    from_date = table.Column<DateTime>(type: "date", nullable: false),
                    to_date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.emp_no);
                });

            migrationBuilder.CreateTable(
                name: "titles_current",
                columns: table => new
                {
                    emp_no = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(maxLength: 50, nullable: false),
                    from_date = table.Column<DateTime>(type: "date", nullable: false),
                    to_date = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.emp_no);
                });

            migrationBuilder.CreateTable(
                name: "dept_emp",
                columns: table => new
                {
                    emp_no = table.Column<int>(type: "int(11)", nullable: false),
                    dept_no = table.Column<string>(fixedLength: true, maxLength: 4, nullable: false),
                    from_date = table.Column<DateTime>(type: "date", nullable: false),
                    to_date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.emp_no, x.dept_no, x.from_date, x.to_date });
                    table.ForeignKey(
                        name: "dept_emp_ibfk_2",
                        column: x => x.dept_no,
                        principalTable: "departments",
                        principalColumn: "dept_no",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "dept_emp_ibfk_1",
                        column: x => x.emp_no,
                        principalTable: "employees",
                        principalColumn: "emp_no",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dept_manager",
                columns: table => new
                {
                    emp_no = table.Column<int>(type: "int(11)", nullable: false),
                    dept_no = table.Column<string>(fixedLength: true, maxLength: 4, nullable: false),
                    from_date = table.Column<DateTime>(type: "date", nullable: false),
                    to_date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.emp_no, x.dept_no, x.from_date, x.to_date });
                    table.ForeignKey(
                        name: "dept_manager_ibfk_2",
                        column: x => x.dept_no,
                        principalTable: "departments",
                        principalColumn: "dept_no",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "dept_manager_ibfk_1",
                        column: x => x.emp_no,
                        principalTable: "employees",
                        principalColumn: "emp_no",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "salaries",
                columns: table => new
                {
                    emp_no = table.Column<int>(type: "int(11)", nullable: false),
                    salary = table.Column<int>(type: "int(11)", nullable: false),
                    from_date = table.Column<DateTime>(type: "date", nullable: false),
                    to_date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.emp_no, x.salary, x.from_date, x.to_date });
                    table.ForeignKey(
                        name: "salaries_ibfk_1",
                        column: x => x.emp_no,
                        principalTable: "employees",
                        principalColumn: "emp_no",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "titles",
                columns: table => new
                {
                    emp_no = table.Column<int>(type: "int(11)", nullable: false),
                    title = table.Column<string>(maxLength: 50, nullable: false),
                    from_date = table.Column<DateTime>(type: "date", nullable: false),
                    to_date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.emp_no, x.title, x.from_date, x.to_date });
                    table.ForeignKey(
                        name: "titles_ibfk_1",
                        column: x => x.emp_no,
                        principalTable: "employees",
                        principalColumn: "emp_no",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "dept_name",
                table: "departments",
                column: "dept_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "dept_no",
                table: "dept_emp",
                column: "dept_no");

            migrationBuilder.CreateIndex(
                name: "dept_emp_emp_no_IDX",
                table: "dept_emp",
                columns: new[] { "emp_no", "to_date" });

            migrationBuilder.CreateIndex(
                name: "dept_emp_current_dept_no_IDX",
                table: "dept_emp_current",
                column: "dept_no");

            migrationBuilder.CreateIndex(
                name: "dept_emp_current_emp_no_IDX",
                table: "dept_emp_current",
                column: "emp_no",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "dept_manager_dept_no_IDX",
                table: "dept_manager",
                columns: new[] { "dept_no", "to_date" });

            migrationBuilder.CreateIndex(
                name: "dept_manager_current_dept_no_IDX",
                table: "dept_manager_current",
                column: "dept_no",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "salaries_emp_no_IDX",
                table: "salaries",
                columns: new[] { "emp_no", "to_date" });

            migrationBuilder.CreateIndex(
                name: "salaries_current_emp_no_IDX",
                table: "salaries_current",
                column: "emp_no",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "salaries_current_salary_IDX",
                table: "salaries_current",
                column: "salary");

            migrationBuilder.CreateIndex(
                name: "titles_emp_no_IDX",
                table: "titles",
                columns: new[] { "emp_no", "to_date" });

            migrationBuilder.CreateIndex(
                name: "titles_current_emp_no_IDX",
                table: "titles_current",
                column: "emp_no",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "titles_current_title_IDX",
                table: "titles_current",
                column: "title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "__efmigrationshistory");

            migrationBuilder.DropTable(
                name: "dept_emp");

            migrationBuilder.DropTable(
                name: "dept_emp_current");

            migrationBuilder.DropTable(
                name: "dept_manager");

            migrationBuilder.DropTable(
                name: "dept_manager_current");

            migrationBuilder.DropTable(
                name: "emp_details_cache");

            migrationBuilder.DropTable(
                name: "salaries");

            migrationBuilder.DropTable(
                name: "salaries_current");

            migrationBuilder.DropTable(
                name: "titles");

            migrationBuilder.DropTable(
                name: "titles_current");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropTable(
                name: "employees");
        }
    }
}
