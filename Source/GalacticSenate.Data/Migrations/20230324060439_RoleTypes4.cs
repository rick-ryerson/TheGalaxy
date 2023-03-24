using Microsoft.EntityFrameworkCore.Migrations;

namespace GalacticSenate.Data.Migrations
{
    public partial class RoleTypes4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parties_PartyRoles_RoleId",
                table: "Parties");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Parties",
                newName: "PartyRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Parties_RoleId",
                table: "Parties",
                newName: "IX_Parties_PartyRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_PartyRoles_PartyRoleId",
                table: "Parties",
                column: "PartyRoleId",
                principalTable: "PartyRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parties_PartyRoles_PartyRoleId",
                table: "Parties");

            migrationBuilder.RenameColumn(
                name: "PartyRoleId",
                table: "Parties",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Parties_PartyRoleId",
                table: "Parties",
                newName: "IX_Parties_RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_PartyRoles_RoleId",
                table: "Parties",
                column: "RoleId",
                principalTable: "PartyRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
