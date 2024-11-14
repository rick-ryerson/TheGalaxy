using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GalacticSenate.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaritalStatusTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatusTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationNameValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationNameValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonNameTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonNameTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonNameValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonNameValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartyRoleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyRoleTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartyRoleTypes_RoleTypes_Id",
                        column: x => x.Id,
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
                        name: "FK_PartyRoles_PartyRoleTypes_PartyRoleTypeId",
                        column: x => x.PartyRoleTypeId,
                        principalTable: "PartyRoleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartyRoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parties_PartyRoles_PartyRoleId",
                        column: x => x.PartyRoleId,
                        principalTable: "PartyRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizations_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationNames",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationNameValueId = table.Column<int>(type: "int", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThruDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationNames", x => new { x.OrganizationId, x.OrganizationNameValueId, x.FromDate });
                    table.ForeignKey(
                        name: "FK_OrganizationNames_OrganizationNameValues_OrganizationNameValueId",
                        column: x => x.OrganizationNameValueId,
                        principalTable: "OrganizationNameValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationNames_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonGenders",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThruDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonGenders", x => new { x.PersonId, x.GenderId, x.FromDate });
                    table.ForeignKey(
                        name: "FK_PersonGenders_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonGenders_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonMaritalStatuses",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaritalStatusId = table.Column<int>(type: "int", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThruDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonMaritalStatuses", x => new { x.PersonId, x.MaritalStatusId, x.FromDate });
                    table.ForeignKey(
                        name: "FK_PersonMaritalStatuses_MaritalStatusTypes_MaritalStatusId",
                        column: x => x.MaritalStatusId,
                        principalTable: "MaritalStatusTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonMaritalStatuses_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonNames",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonNameValueId = table.Column<int>(type: "int", nullable: false),
                    PersonNameTypeId = table.Column<int>(type: "int", nullable: false),
                    Ordinal = table.Column<int>(type: "int", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThruDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonNames", x => new { x.PersonId, x.PersonNameValueId, x.PersonNameTypeId, x.Ordinal, x.FromDate });
                    table.ForeignKey(
                        name: "FK_PersonNames_PersonNameTypes_PersonNameTypeId",
                        column: x => x.PersonNameTypeId,
                        principalTable: "PersonNameTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonNames_PersonNameValues_PersonNameValueId",
                        column: x => x.PersonNameValueId,
                        principalTable: "PersonNameValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonNames_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genders_Value",
                table: "Genders",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaritalStatusTypes_Value",
                table: "MaritalStatusTypes",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationNames_OrganizationId_OrganizationNameValueId_FromDate_ThruDate",
                table: "OrganizationNames",
                columns: new[] { "OrganizationId", "OrganizationNameValueId", "FromDate", "ThruDate" },
                unique: true,
                filter: "[ThruDate] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationNames_OrganizationNameValueId",
                table: "OrganizationNames",
                column: "OrganizationNameValueId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationNameValues_Value",
                table: "OrganizationNameValues",
                column: "Value",
                unique: true,
                filter: "[Value] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_PartyId",
                table: "Organizations",
                column: "PartyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parties_PartyRoleId",
                table: "Parties",
                column: "PartyRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PartyRoles_PartyRoleTypeId",
                table: "PartyRoles",
                column: "PartyRoleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonGenders_GenderId",
                table: "PersonGenders",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonGenders_PersonId_GenderId_FromDate_ThruDate",
                table: "PersonGenders",
                columns: new[] { "PersonId", "GenderId", "FromDate", "ThruDate" },
                unique: true,
                filter: "[ThruDate] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PersonMaritalStatuses_MaritalStatusId",
                table: "PersonMaritalStatuses",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonMaritalStatuses_PersonId_MaritalStatusId_FromDate_ThruDate",
                table: "PersonMaritalStatuses",
                columns: new[] { "PersonId", "MaritalStatusId", "FromDate", "ThruDate" },
                unique: true,
                filter: "[ThruDate] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PersonNames_PersonId_PersonNameValueId_PersonNameTypeId_Ordinal_FromDate_ThruDate",
                table: "PersonNames",
                columns: new[] { "PersonId", "PersonNameValueId", "PersonNameTypeId", "Ordinal", "FromDate", "ThruDate" },
                unique: true,
                filter: "[ThruDate] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PersonNames_PersonNameTypeId",
                table: "PersonNames",
                column: "PersonNameTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonNames_PersonNameValueId",
                table: "PersonNames",
                column: "PersonNameValueId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonNameTypes_Value",
                table: "PersonNameTypes",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonNameValues_Value",
                table: "PersonNameValues",
                column: "Value",
                unique: true,
                filter: "[Value] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PartyId",
                table: "Persons",
                column: "PartyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleTypes_Description",
                table: "RoleTypes",
                column: "Description",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizationNames");

            migrationBuilder.DropTable(
                name: "PersonGenders");

            migrationBuilder.DropTable(
                name: "PersonMaritalStatuses");

            migrationBuilder.DropTable(
                name: "PersonNames");

            migrationBuilder.DropTable(
                name: "OrganizationNameValues");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "MaritalStatusTypes");

            migrationBuilder.DropTable(
                name: "PersonNameTypes");

            migrationBuilder.DropTable(
                name: "PersonNameValues");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Parties");

            migrationBuilder.DropTable(
                name: "PartyRoles");

            migrationBuilder.DropTable(
                name: "PartyRoleTypes");

            migrationBuilder.DropTable(
                name: "RoleTypes");
        }
    }
}
