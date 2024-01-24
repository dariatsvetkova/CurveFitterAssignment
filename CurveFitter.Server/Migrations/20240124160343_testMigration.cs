using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CurveFitter.Server.Migrations
{
    public partial class testMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Archives",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    FitType = table.Column<string>(nullable: false),
                    Equation = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Archives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataPoint",
                columns: table => new
                {
                    X = table.Column<double>(nullable: false),
                    Y = table.Column<double>(nullable: false),
                    ArchiveId = table.Column<int>(nullable: true),
                    ArchiveId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataPoint", x => x.X);
                    table.ForeignKey(
                        name: "FK_DataPoint_Archives_ArchiveId",
                        column: x => x.ArchiveId,
                        principalTable: "Archives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DataPoint_Archives_ArchiveId1",
                        column: x => x.ArchiveId1,
                        principalTable: "Archives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataPoint_ArchiveId",
                table: "DataPoint",
                column: "ArchiveId");

            migrationBuilder.CreateIndex(
                name: "IX_DataPoint_ArchiveId1",
                table: "DataPoint",
                column: "ArchiveId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataPoint");

            migrationBuilder.DropTable(
                name: "Archives");
        }
    }
}
