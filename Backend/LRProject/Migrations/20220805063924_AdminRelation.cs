using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LRProject.Migrations
{
    /// <inheritdoc />
    public partial class AdminRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "SourceGroups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SourceGroups_EmployeeId",
                table: "SourceGroups",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SourceGroups_Employees_EmployeeId",
                table: "SourceGroups",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SourceGroups_Employees_EmployeeId",
                table: "SourceGroups");

            migrationBuilder.DropIndex(
                name: "IX_SourceGroups_EmployeeId",
                table: "SourceGroups");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "SourceGroups");
        }
    }
}
