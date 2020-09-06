using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskMaster.DataAccess.Migrations
{
    public partial class AddUsertoDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Password = table.Column<string>(nullable: true),
                    AccessLevel = table.Column<string>(nullable: true),
                    Dept = table.Column<string>(nullable: true),
                    ts = table.Column<DateTime>(nullable: false),
                    df = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Login");
        }
    }
}
