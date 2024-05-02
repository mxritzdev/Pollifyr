using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pollifyr.App.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddAttendable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Attendable",
                table: "Surveys",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attendable",
                table: "Surveys");
        }
    }
}
