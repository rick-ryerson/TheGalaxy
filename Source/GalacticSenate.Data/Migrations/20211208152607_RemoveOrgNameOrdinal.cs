using Microsoft.EntityFrameworkCore.Migrations;

namespace GalacticSenate.Data.Migrations
{
    public partial class RemoveOrgNameOrdinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizationNames",
                table: "OrganizationNames");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationNames_Ordinal_OrganizationId_OrganizationNameValueId_FromDate_ThruDate",
                table: "OrganizationNames");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationNames_OrganizationId",
                table: "OrganizationNames");

            migrationBuilder.DropColumn(
                name: "Ordinal",
                table: "OrganizationNames");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "PersonNameValues",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizationNames",
                table: "OrganizationNames",
                columns: new[] { "OrganizationId", "OrganizationNameValueId", "FromDate" });

            migrationBuilder.CreateIndex(
                name: "IX_PersonNameValues_Value",
                table: "PersonNameValues",
                column: "Value",
                unique: true,
                filter: "[Value] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationNames_OrganizationId_OrganizationNameValueId_FromDate_ThruDate",
                table: "OrganizationNames",
                columns: new[] { "OrganizationId", "OrganizationNameValueId", "FromDate", "ThruDate" },
                unique: true,
                filter: "[ThruDate] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PersonNameValues_Value",
                table: "PersonNameValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizationNames",
                table: "OrganizationNames");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationNames_OrganizationId_OrganizationNameValueId_FromDate_ThruDate",
                table: "OrganizationNames");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "PersonNameValues",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ordinal",
                table: "OrganizationNames",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizationNames",
                table: "OrganizationNames",
                columns: new[] { "Ordinal", "OrganizationId", "OrganizationNameValueId", "FromDate" });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationNames_Ordinal_OrganizationId_OrganizationNameValueId_FromDate_ThruDate",
                table: "OrganizationNames",
                columns: new[] { "Ordinal", "OrganizationId", "OrganizationNameValueId", "FromDate", "ThruDate" },
                unique: true,
                filter: "[ThruDate] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationNames_OrganizationId",
                table: "OrganizationNames",
                column: "OrganizationId");
        }
    }
}
