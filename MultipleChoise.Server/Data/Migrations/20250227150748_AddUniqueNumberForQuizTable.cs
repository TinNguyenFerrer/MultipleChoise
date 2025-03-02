using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultipleChoise.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueNumberForQuizTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UniqueNumber",
                table: "Quizzes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql(@"
                WITH Ranked AS (
                    SELECT Id, ROW_NUMBER() OVER (ORDER BY Id) + 100 AS NewUniqueNumber
                    FROM Quizzes
                )
                UPDATE Quizzes
                SET UniqueNumber = (SELECT NewUniqueNumber FROM Ranked WHERE Ranked.Id = Quizzes.Id);
            ");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_UniqueNumber",
                table: "Quizzes",
                column: "UniqueNumber",
                unique: true);

            migrationBuilder.Sql(@"
                CREATE TRIGGER SetUniqueNumberAfterInsert
                AFTER INSERT ON Quizzes
                FOR EACH ROW
                WHEN NEW.UniqueNumber = 0 OR NEW.UniqueNumber IS NULL
                BEGIN
                    UPDATE Quizzes 
                    SET UniqueNumber = (SELECT IFNULL(MAX(UniqueNumber), 99) + 1 FROM Quizzes)
                    WHERE Id = NEW.Id;
                END;
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(name: "IX_Quizzes_UniqueNumber", table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "UniqueNumber",
                table: "Quizzes");

            migrationBuilder.Sql("DROP TRIGGER IF EXISTS SetUniqueNumberBeforeInsert;");
        }
    }
}
