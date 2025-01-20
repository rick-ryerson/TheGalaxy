using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalacticSenate.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedFamilies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartyRoleTypes_RoleTypes_Id",
                table: "PartyRoleTypes");

            migrationBuilder.CreateTable(
                name: "InformalOrganizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformalOrganizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InformalOrganizations_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Families",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InformalOrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Families", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Families_InformalOrganizations_InformalOrganizationId",
                        column: x => x.InformalOrganizationId,
                        principalTable: "InformalOrganizations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Families_InformalOrganizationId",
                table: "Families",
                column: "InformalOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_InformalOrganizations_OrganizationId",
                table: "InformalOrganizations",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PartyRoleTypes_RoleTypes_Id",
                table: "PartyRoleTypes",
                column: "Id",
                principalTable: "RoleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartyRoleTypes_RoleTypes_Id",
                table: "PartyRoleTypes");

            migrationBuilder.DropTable(
                name: "Families");

            migrationBuilder.DropTable(
                name: "InformalOrganizations");

            migrationBuilder.AddForeignKey(
                name: "FK_PartyRoleTypes_RoleTypes_Id",
                table: "PartyRoleTypes",
                column: "Id",
                principalTable: "RoleTypes",
                principalColumn: "Id");
        }
    }
}
