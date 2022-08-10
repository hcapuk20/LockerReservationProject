using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LRProject.Migrations
{
    /// <inheritdoc />
    public partial class addedSourceSpace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Space",
                table: "Sources",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Space",
                table: "Sources");
        }
    }
}
