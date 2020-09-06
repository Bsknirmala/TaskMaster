using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskMaster.DataAccess.Migrations
{
    public partial class StoreProcforUservalidation : Migration

    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROC usp_loginvalidation
                                    @Name varchar(20),
                                    @Password varchar(20)
                                    AS 
                                    BEGIN 
                                     SELECT * FROM dbo.Login  WHERE  (Name = @Name and Password=@Password) 
                                    END ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE  usp_loginvalidation");
        }
    }
}
