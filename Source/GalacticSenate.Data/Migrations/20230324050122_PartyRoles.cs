using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GalacticSenate.Data.Migrations
{
    public partial class PartyRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Parties",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RoleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartyRoleTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleTypes_RoleTypes_PartyRoleTypeId",
                        column: x => x.PartyRoleTypeId,
                        principalTable: "RoleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PartyRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Thru = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PartyRoleTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartyRoles_RoleTypes_PartyRoleTypeId",
                        column: x => x.PartyRoleTypeId,
                        principalTable: "RoleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parties_RoleId",
                table: "Parties",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PartyRoles_PartyRoleTypeId",
                table: "PartyRoles",
                column: "PartyRoleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleTypes_Description",
                table: "RoleTypes",
                column: "Description",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleTypes_PartyRoleTypeId",
                table: "RoleTypes",
                column: "PartyRoleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_PartyRoles_RoleId",
                table: "Parties",
                column: "RoleId",
                principalTable: "PartyRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parties_PartyRoles_RoleId",
                table: "Parties");

            migrationBuilder.DropTable(
                name: "PartyRoles");

            migrationBuilder.DropTable(
                name: "RoleTypes");

            migrationBuilder.DropIndex(
                name: "IX_Parties_RoleId",
                table: "Parties");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Parties");
        }
    }
}
