using Microsoft.EntityFrameworkCore.Migrations;

namespace PeopleAndOrganizations.Data.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonNameTypeId",
                table: "PersonNames",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PersonNames_PersonNameTypeId",
                table: "PersonNames",
                column: "PersonNameTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonNames_PersonNameTypes_PersonNameTypeId",
                table: "PersonNames",
                column: "PersonNameTypeId",
                principalTable: "PersonNameTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonNames_PersonNameTypes_PersonNameTypeId",
                table: "PersonNames");

            migrationBuilder.DropIndex(
                name: "IX_PersonNames_PersonNameTypeId",
                table: "PersonNames");

            migrationBuilder.DropColumn(
                name: "PersonNameTypeId",
                table: "PersonNames");
        }
    }
}
