using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUDWithStoredProceduresDemo.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryID);
                });

            migrationBuilder.Sql(@"
CREATE PROCEDURE usp_GetAllCountries
AS
BEGIN
SELECT * from Country Order By CountryName
END;
GO
CREATE PROCEDURE usp_GetCountryDetailsById
(@CountryID int)
AS
BEGIN
SELECT * from country where countryid=@CountryID
END;
GO

CREATE PROCEDURE usp_Country_Insert
(@CountryName varchar(50),
@CountryID int out)
AS
BEGIN
INSERT INTO Country(CountryName) VALUES(@CountryName)
SELECT @CountryID=@@IDENTITY FROM COUNTRY
END;


GO
CREATE PROCEDURE usp_Country_Update
(@CountryID int,@CountryName varchar(50))
AS
BEGIN
UPDATE Country set countryname=@CountryName
WHERE CountryID=@CountryID
END;
GO
CREATE PROCEDURE usp_Country_Delete
(@CountryID int)
AS
BEGIN
DELETE FROM Country WHERE CountryID=@CountryID
END;

");




        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
