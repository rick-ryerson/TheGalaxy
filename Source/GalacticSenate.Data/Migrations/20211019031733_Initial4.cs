using Microsoft.EntityFrameworkCore.Migrations;

namespace GalacticSenate.Data.Migrations
{
    public partial class Initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonMaritalStatuses_MaritalStatuses_MaritalStatusId",
                table: "PersonMaritalStatuses");

            migrationBuilder.DropTable(
                name: "MaritalStatuses");

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

            migrationBuilder.CreateIndex(
                name: "IX_MaritalStatusTypes_Value",
                table: "MaritalStatusTypes",
                column: "Value",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonMaritalStatuses_MaritalStatusTypes_MaritalStatusId",
                table: "PersonMaritalStatuses",
                column: "MaritalStatusId",
                principalTable: "MaritalStatusTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonMaritalStatuses_MaritalStatusTypes_MaritalStatusId",
                table: "PersonMaritalStatuses");

            migrationBuilder.DropTable(
                name: "MaritalStatusTypes");

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

            migrationBuilder.CreateIndex(
                name: "IX_MaritalStatuses_Value",
                table: "MaritalStatuses",
                column: "Value",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonMaritalStatuses_MaritalStatuses_MaritalStatusId",
                table: "PersonMaritalStatuses",
                column: "MaritalStatusId",
                principalTable: "MaritalStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
