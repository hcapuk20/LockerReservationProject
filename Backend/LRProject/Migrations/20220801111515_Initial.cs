﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LRProject.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SourceGroups",
                columns: table => new
                {
                    Source_Group_Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceGroups", x => x.Source_Group_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sources",
                columns: table => new
                {
                    Source_Id = table.Column<int>(type: "int", nullable: false),
                    SourceGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sources", x => x.Source_Id);
                    table.ForeignKey(
                        name: "FK_Sources_SourceGroups_SourceGroupId",
                        column: x => x.SourceGroupId,
                        principalTable: "SourceGroups",
                        principalColumn: "Source_Group_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sources_SourceGroupId",
                table: "Sources",
                column: "SourceGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sources");

            migrationBuilder.DropTable(
                name: "SourceGroups");
        }
    }
}
