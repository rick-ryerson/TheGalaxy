using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GalacticSenate.Data.Migrations
{
    public partial class Initial5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonNames",
                table: "PersonNames");

            migrationBuilder.DropIndex(
                name: "IX_PersonNames_PersonId_PersonNameValueId_Ordinal_FromDate_ThruDate",
                table: "PersonNames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonMaritalStatuses",
                table: "PersonMaritalStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonGenders",
                table: "PersonGenders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ThruDate",
                table: "PersonMaritalStatuses",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ThruDate",
                table: "PersonGenders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonNames",
                table: "PersonNames",
                columns: new[] { "PersonId", "PersonNameValueId", "PersonNameTypeId", "Ordinal", "FromDate" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonMaritalStatuses",
                table: "PersonMaritalStatuses",
                columns: new[] { "PersonId", "MaritalStatusId", "FromDate" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonGenders",
                table: "PersonGenders",
                columns: new[] { "PersonId", "GenderId", "FromDate" });

            migrationBuilder.CreateIndex(
                name: "IX_PersonNames_PersonId_PersonNameValueId_PersonNameTypeId_Ordinal_FromDate_ThruDate",
                table: "PersonNames",
                columns: new[] { "PersonId", "PersonNameValueId", "PersonNameTypeId", "Ordinal", "FromDate", "ThruDate" },
                unique: true,
                filter: "[ThruDate] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PersonMaritalStatuses_PersonId_MaritalStatusId_FromDate_ThruDate",
                table: "PersonMaritalStatuses",
                columns: new[] { "PersonId", "MaritalStatusId", "FromDate", "ThruDate" },
                unique: true,
                filter: "[ThruDate] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PersonGenders_PersonId_GenderId_FromDate_ThruDate",
                table: "PersonGenders",
                columns: new[] { "PersonId", "GenderId", "FromDate", "ThruDate" },
                unique: true,
                filter: "[ThruDate] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonNames",
                table: "PersonNames");

            migrationBuilder.DropIndex(
                name: "IX_PersonNames_PersonId_PersonNameValueId_PersonNameTypeId_Ordinal_FromDate_ThruDate",
                table: "PersonNames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonMaritalStatuses",
                table: "PersonMaritalStatuses");

            migrationBuilder.DropIndex(
                name: "IX_PersonMaritalStatuses_PersonId_MaritalStatusId_FromDate_ThruDate",
                table: "PersonMaritalStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonGenders",
                table: "PersonGenders");

            migrationBuilder.DropIndex(
                name: "IX_PersonGenders_PersonId_GenderId_FromDate_ThruDate",
                table: "PersonGenders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ThruDate",
                table: "PersonMaritalStatuses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ThruDate",
                table: "PersonGenders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonNames",
                table: "PersonNames",
                columns: new[] { "PersonId", "PersonNameValueId", "Ordinal", "FromDate" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonMaritalStatuses",
                table: "PersonMaritalStatuses",
                columns: new[] { "PersonId", "MaritalStatusId", "FromDate", "ThruDate" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonGenders",
                table: "PersonGenders",
                columns: new[] { "PersonId", "GenderId", "FromDate", "ThruDate" });

            migrationBuilder.CreateIndex(
                name: "IX_PersonNames_PersonId_PersonNameValueId_Ordinal_FromDate_ThruDate",
                table: "PersonNames",
                columns: new[] { "PersonId", "PersonNameValueId", "Ordinal", "FromDate", "ThruDate" },
                unique: true,
                filter: "[ThruDate] IS NOT NULL");
        }
    }
}
