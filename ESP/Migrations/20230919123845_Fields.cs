using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class Fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Processes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastRevisionStatus",
                table: "Processes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryConnectionStatus",
                table: "Processes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PrimaryDate",
                table: "Processes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Processes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDateLastRevision",
                table: "Processes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusDate",
                table: "Processes",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "LastRevisionStatus",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "PrimaryConnectionStatus",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "PrimaryDate",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "StartDateLastRevision",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "StatusDate",
                table: "Processes");
        }
    }
}
