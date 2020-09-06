using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskMaster.DataAccess.Migrations
{
    public partial class AddIssuestabletoDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                           name: "Issues",
                           columns: table => new
                           {
                               Id = table.Column<int>(nullable: false)
                                   .Annotation("SqlServer:Identity", "1, 1"),
                               Description = table.Column<string>(maxLength: 200, nullable: false),
                               Reportedby = table.Column<string>(nullable: false),
                               Issuedt = table.Column<DateTime>(nullable: false),
                               Action = table.Column<string>(nullable: true),
                               Issuestatus = table.Column<string>(nullable: true),
                               ts = table.Column<DateTime>(nullable: false),
                               df = table.Column<DateTime>(nullable: false),
                               CarparkID = table.Column<int>(nullable: false),
                               GantryID = table.Column<int>(nullable: false),
                               CategoryID = table.Column<int>(nullable: false)
                           },
                           constraints: table =>
                           {
                               table.PrimaryKey("PK_Issues", x => x.Id);
                               table.ForeignKey(
                               name: "FK_Issues_Carparks_CarparkID",
                               column: x => x.CarparkID,
                               principalTable: "Carparks",
                               principalColumn: "Id",
                               onDelete: ReferentialAction.Cascade);
                               table.ForeignKey(
                               name: "FK_Issues_Hardwaretypes_CategoryID",
                               column: x => x.CategoryID,
                               principalTable: "Hardwaretypes",
                               principalColumn: "Id",
                               onDelete: ReferentialAction.Cascade);
                           });
            migrationBuilder.CreateIndex(
            name: "IX_Issues_CarparkID",
            table: "Issues",
            column: "CarparkID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "Issues");
        }
    }
}
