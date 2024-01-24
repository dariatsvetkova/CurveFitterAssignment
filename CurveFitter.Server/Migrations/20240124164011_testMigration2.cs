using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CurveFitter.Server.Migrations
{
    public partial class testMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Y",
                table: "DataPoint",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<int>(
                name: "X",
                table: "DataPoint",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<string>(
                name: "Equation",
                table: "Archives",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Y",
                table: "DataPoint",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "X",
                table: "DataPoint",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Equation",
                table: "Archives",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
