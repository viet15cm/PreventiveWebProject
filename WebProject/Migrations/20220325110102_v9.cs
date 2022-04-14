using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProject.Migrations
{
    public partial class v9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nane",
                table: "categorizes",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "classifies",
                type: "Char(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "categorizes",
                type: "Char(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_classifies_Code",
                table: "classifies",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_categorizes_Code",
                table: "categorizes",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_classifies_Code",
                table: "classifies");

            migrationBuilder.DropIndex(
                name: "IX_categorizes_Code",
                table: "categorizes");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "classifies");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "categorizes");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "categorizes",
                newName: "Nane");
        }
    }
}
