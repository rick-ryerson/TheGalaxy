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
                name: "MaritalStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NameTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NameTypes", x => x.Id);
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
                name: "Parties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonNameValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonNameValues", x => x.Id);
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
                    Ordinal = table.Column<int>(type: "int", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThruDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationNames", x => new { x.Ordinal, x.OrganizationId, x.OrganizationNameValueId, x.FromDate });
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
                    ThruDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonGenders", x => new { x.PersonId, x.GenderId, x.FromDate, x.ThruDate });
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
                    ThruDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonMaritalStatuses", x => new { x.PersonId, x.MaritalStatusId, x.FromDate, x.ThruDate });
                    table.ForeignKey(
                        name: "FK_PersonMaritalStatuses_MaritalStatuses_MaritalStatusId",
                        column: x => x.MaritalStatusId,
                        principalTable: "MaritalStatuses",
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
                    Ordinal = table.Column<int>(type: "int", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThruDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonNames", x => new { x.PersonId, x.PersonNameValueId, x.Ordinal, x.FromDate });
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
                name: "IX_MaritalStatuses_Value",
                table: "MaritalStatuses",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NameTypes_Value",
                table: "NameTypes",
                column: "Value",
                unique: true);

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
                name: "IX_PersonGenders_GenderId",
                table: "PersonGenders",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonMaritalStatuses_MaritalStatusId",
                table: "PersonMaritalStatuses",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonNames_PersonId_PersonNameValueId_Ordinal_FromDate_ThruDate",
                table: "PersonNames",
                columns: new[] { "PersonId", "PersonNameValueId", "Ordinal", "FromDate", "ThruDate" },
                unique: true,
                filter: "[ThruDate] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PersonNames_PersonNameValueId",
                table: "PersonNames",
                column: "PersonNameValueId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PartyId",
                table: "Persons",
                column: "PartyId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NameTypes");

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
                name: "MaritalStatuses");

            migrationBuilder.DropTable(
                name: "PersonNameValues");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Parties");
        }
    }
}
