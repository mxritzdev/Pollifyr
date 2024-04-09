using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pollifyr.App.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddSortingRowAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SortingId",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortingId",
                table: "Answers");
        }
    }
}
