using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class updateProcess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Revisions_BlockTestId",
                table: "Revisions");

            migrationBuilder.DropIndex(
                name: "IX_Revisions_IntegrationId",
                table: "Revisions");

            migrationBuilder.DropIndex(
                name: "IX_Revisions_PersonId",
                table: "Revisions");

            migrationBuilder.DropIndex(
                name: "IX_Revisions_ProcessInfoBlockId",
                table: "Revisions");

            migrationBuilder.DropIndex(
                name: "IX_Revisions_TechnologistBlockId",
                table: "Revisions");

            migrationBuilder.AlterColumn<string>(
                name: "StatusDate",
                table: "Processes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StartDateLastRevision",
                table: "Processes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StartDate",
                table: "Processes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PrimaryDate",
                table: "Processes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastDateRevision",
                table: "Processes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Revisions_BlockTestId",
                table: "Revisions",
                column: "BlockTestId",
                unique: true,
                filter: "[BlockTestId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Revisions_IntegrationId",
                table: "Revisions",
                column: "IntegrationId",
                unique: true,
                filter: "[IntegrationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Revisions_PersonId",
                table: "Revisions",
                column: "PersonId",
                unique: true,
                filter: "[PersonId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Revisions_ProcessInfoBlockId",
                table: "Revisions",
                column: "ProcessInfoBlockId",
                unique: true,
                filter: "[ProcessInfoBlockId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Revisions_TechnologistBlockId",
                table: "Revisions",
                column: "TechnologistBlockId",
                unique: true,
                filter: "[TechnologistBlockId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Revisions_BlockTestId",
                table: "Revisions");

            migrationBuilder.DropIndex(
                name: "IX_Revisions_IntegrationId",
                table: "Revisions");

            migrationBuilder.DropIndex(
                name: "IX_Revisions_PersonId",
                table: "Revisions");

            migrationBuilder.DropIndex(
                name: "IX_Revisions_ProcessInfoBlockId",
                table: "Revisions");

            migrationBuilder.DropIndex(
                name: "IX_Revisions_TechnologistBlockId",
                table: "Revisions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StatusDate",
                table: "Processes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDateLastRevision",
                table: "Processes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Processes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PrimaryDate",
                table: "Processes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastDateRevision",
                table: "Processes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Revisions_BlockTestId",
                table: "Revisions",
                column: "BlockTestId");

            migrationBuilder.CreateIndex(
                name: "IX_Revisions_IntegrationId",
                table: "Revisions",
                column: "IntegrationId");

            migrationBuilder.CreateIndex(
                name: "IX_Revisions_PersonId",
                table: "Revisions",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Revisions_ProcessInfoBlockId",
                table: "Revisions",
                column: "ProcessInfoBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_Revisions_TechnologistBlockId",
                table: "Revisions",
                column: "TechnologistBlockId");
        }
    }
}
