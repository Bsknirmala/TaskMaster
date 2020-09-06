using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskMaster.DataAccess.Migrations
{
    public partial class AddStoredProcGetGantrydetailsbyCarparkID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROC usp_GetGantrydetailbyCarpark
                                    @CarparkID int
                                    AS 
                                    BEGIN 
                                     SELECT * FROM dbo.Gantries  WHERE  (CarparkID = @CarparkID) 
                                    END ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetGantrydetailbyCarpark");
        }
    }
}
