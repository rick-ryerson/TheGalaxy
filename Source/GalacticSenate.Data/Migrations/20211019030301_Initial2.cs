using Microsoft.EntityFrameworkCore.Migrations;

namespace GalacticSenate.Data.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NameTypes");

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

            migrationBuilder.CreateIndex(
                name: "IX_PersonNameTypes_Value",
                table: "PersonNameTypes",
                column: "Value",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonNameTypes");

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

            migrationBuilder.CreateIndex(
                name: "IX_NameTypes_Value",
                table: "NameTypes",
                column: "Value",
                unique: true);
        }
    }
}
