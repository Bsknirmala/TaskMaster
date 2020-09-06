using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskMaster.DataAccess.Migrations
{
    public partial class AddGantryHardwaretoDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gantries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    Prefix = table.Column<string>(nullable: false),
                    ts = table.Column<DateTime>(nullable: false),
                    df = table.Column<DateTime>(nullable: false),
                    CarparkID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gantries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gantries_Carparks_CarparkID",
                        column: x => x.CarparkID,
                        principalTable: "Carparks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hardwaretypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    ts = table.Column<DateTime>(nullable: false),
                    df = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hardwaretypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gantries_CarparkID",
                table: "Gantries",
                column: "CarparkID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gantries");

            migrationBuilder.DropTable(
                name: "Hardwaretypes");
        }
    }
}
