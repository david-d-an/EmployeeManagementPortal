// using Microsoft.EntityFrameworkCore.Migrations;

// namespace EMP.DbScaffold.Migrations.STS
// {
//     public partial class InitialDb : Migration
//     {
//         protected override void Up(MigrationBuilder migrationBuilder)
//         {
//             migrationBuilder.CreateTable(
//                 name: "users",
//                 columns: table => new
//                 {
//                     user_name = table.Column<string>(maxLength: 40, nullable: false),
//                     password = table.Column<string>(maxLength: 50, nullable: false),
//                     user_id = table.Column<int>(type: "int(11)", nullable: false),
//                     hashed_password = table.Column<string>(maxLength: 512, nullable: true),
//                     locked = table.Column<bool>(type: "bool", nullable: true)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PRIMARY", x => new { x.user_name, x.password });
//                 });

//             migrationBuilder.CreateIndex(
//                 name: "users",
//                 table: "users",
//                 columns: new[] { "user_name", "password" },
//                 unique: true);
//         }

//         protected override void Down(MigrationBuilder migrationBuilder)
//         {
//             migrationBuilder.DropTable(
//                 name: "users");
//         }
//     }
// }
