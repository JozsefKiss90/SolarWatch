using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolarWatch.Migrations
{
    public partial class GetSunriseMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Sunrise_SunriseId",
                table: "Cities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sunrise",
                table: "Sunrise");

            migrationBuilder.RenameTable(
                name: "Sunrise",
                newName: "Sunrises");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "SunriseTimeDb",
                table: "Sunrises",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sunrises",
                table: "Sunrises",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Sunrises_SunriseId",
                table: "Cities",
                column: "SunriseId",
                principalTable: "Sunrises",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Sunrises_SunriseId",
                table: "Cities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sunrises",
                table: "Sunrises");

            migrationBuilder.RenameTable(
                name: "Sunrises",
                newName: "Sunrise");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SunriseTimeDb",
                table: "Sunrise",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sunrise",
                table: "Sunrise",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Sunrise_SunriseId",
                table: "Cities",
                column: "SunriseId",
                principalTable: "Sunrise",
                principalColumn: "Id");
        }
    }
}
