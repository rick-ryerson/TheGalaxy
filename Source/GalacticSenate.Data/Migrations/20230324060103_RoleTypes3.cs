using Microsoft.EntityFrameworkCore.Migrations;

namespace GalacticSenate.Data.Migrations
{
    public partial class RoleTypes3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartyRoleTypes_PartyRoleTypes_PartyRoleTypeId",
                table: "PartyRoleTypes");

            migrationBuilder.DropIndex(
                name: "IX_PartyRoleTypes_PartyRoleTypeId",
                table: "PartyRoleTypes");

            migrationBuilder.DropColumn(
                name: "PartyRoleTypeId",
                table: "PartyRoleTypes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PartyRoleTypeId",
                table: "PartyRoleTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PartyRoleTypes_PartyRoleTypeId",
                table: "PartyRoleTypes",
                column: "PartyRoleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PartyRoleTypes_PartyRoleTypes_PartyRoleTypeId",
                table: "PartyRoleTypes",
                column: "PartyRoleTypeId",
                principalTable: "PartyRoleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
