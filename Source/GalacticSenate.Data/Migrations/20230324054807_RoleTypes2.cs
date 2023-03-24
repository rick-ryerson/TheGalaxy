using Microsoft.EntityFrameworkCore.Migrations;

namespace GalacticSenate.Data.Migrations
{
    public partial class RoleTypes2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartyRoles_RoleTypes_PartyRoleTypeId",
                table: "PartyRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleTypes_RoleTypes_PartyRoleTypeId",
                table: "RoleTypes");

            migrationBuilder.DropIndex(
                name: "IX_RoleTypes_PartyRoleTypeId",
                table: "RoleTypes");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "RoleTypes");

            migrationBuilder.DropColumn(
                name: "PartyRoleTypeId",
                table: "RoleTypes");

            migrationBuilder.CreateTable(
                name: "PartyRoleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    PartyRoleTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyRoleTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartyRoleTypes_PartyRoleTypes_PartyRoleTypeId",
                        column: x => x.PartyRoleTypeId,
                        principalTable: "PartyRoleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartyRoleTypes_RoleTypes_Id",
                        column: x => x.Id,
                        principalTable: "RoleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartyRoleTypes_PartyRoleTypeId",
                table: "PartyRoleTypes",
                column: "PartyRoleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PartyRoles_PartyRoleTypes_PartyRoleTypeId",
                table: "PartyRoles",
                column: "PartyRoleTypeId",
                principalTable: "PartyRoleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartyRoles_PartyRoleTypes_PartyRoleTypeId",
                table: "PartyRoles");

            migrationBuilder.DropTable(
                name: "PartyRoleTypes");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "RoleTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PartyRoleTypeId",
                table: "RoleTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleTypes_PartyRoleTypeId",
                table: "RoleTypes",
                column: "PartyRoleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PartyRoles_RoleTypes_PartyRoleTypeId",
                table: "PartyRoles",
                column: "PartyRoleTypeId",
                principalTable: "RoleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleTypes_RoleTypes_PartyRoleTypeId",
                table: "RoleTypes",
                column: "PartyRoleTypeId",
                principalTable: "RoleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
