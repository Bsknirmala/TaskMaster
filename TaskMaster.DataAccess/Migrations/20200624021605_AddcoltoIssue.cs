using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskMaster.DataAccess.Migrations
{
    public partial class AddcoltoIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServiceRptno",
                table: "Issues",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_GantryID",
                table: "Issues",
                column: "GantryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropIndex(
                name: "IX_Issues_GantryID",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ServiceRptno",
                table: "Issues");
        }
    }
}
