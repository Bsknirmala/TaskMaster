using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskMaster.DataAccess.Migrations
{
    public partial class AddStoredProcforGantrybyCarpark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROC usp_GetGantrybyCarpark
                                    @CarparkID int
                                    AS 
                                    BEGIN 
                                     SELECT Id FROM dbo.Gantries  WHERE  (CarparkID = @CarparkID) 
                                    END ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetGantrybyCarpark");
        }
    }
}
