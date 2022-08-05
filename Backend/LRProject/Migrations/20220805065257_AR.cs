using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LRProject.Migrations
{
    /// <inheritdoc />
    public partial class AR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "EmployeeSourceGroup",
                columns: table => new
                {
                    EmployeesId = table.Column<int>(type: "int", nullable: false),
                    SourceGroupsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSourceGroup", x => new { x.EmployeesId, x.SourceGroupsId });
                    table.ForeignKey(
                        name: "FK_EmployeeSourceGroup_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeSourceGroup_SourceGroups_SourceGroupsId",
                        column: x => x.SourceGroupsId,
                        principalTable: "SourceGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSourceGroup_SourceGroupsId",
                table: "EmployeeSourceGroup",
                column: "SourceGroupsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeSourceGroup");

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
    }
}
