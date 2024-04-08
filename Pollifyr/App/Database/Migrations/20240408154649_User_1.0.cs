using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pollifyr.App.Database.Migrations
{
    /// <inheritdoc />
    public partial class User_10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompletedSurveys",
                table: "Users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedSurveys",
                table: "Users");
        }
    }
}
