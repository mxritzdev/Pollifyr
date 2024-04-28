using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pollifyr.App.Database.Migrations
{
    /// <inheritdoc />
    public partial class addedAttends : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Attends",
                table: "Surveys",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attends",
                table: "Surveys");
        }
    }
}
