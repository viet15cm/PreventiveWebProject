using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProject.Migrations
{
    public partial class v10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_commodities_classifies_ClassifyId",
                table: "commodities");

            migrationBuilder.DropForeignKey(
                name: "FK_dataImages_categorizes_CategorizeId",
                table: "dataImages");

            migrationBuilder.DropForeignKey(
                name: "FK_products_categorizes_CategorizeId",
                table: "products");

            migrationBuilder.DropTable(
                name: "classifies");

            migrationBuilder.DropIndex(
                name: "IX_products_CategorizeId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_commodities_ClassifyId",
                table: "commodities");

            migrationBuilder.DropColumn(
                name: "CategorizeId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "ClassifyId",
                table: "commodities");

            migrationBuilder.RenameColumn(
                name: "CategorizeId",
                table: "dataImages",
                newName: "LinesId");

            migrationBuilder.RenameIndex(
                name: "IX_dataImages_CategorizeId",
                table: "dataImages",
                newName: "IX_dataImages_LinesId");

            migrationBuilder.AddColumn<Guid>(
                name: "LinesId",
                table: "products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "categorizes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<string>(type: "char(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<string>(type: "Char(5)", maxLength: 5, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategorizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lines_categorizes_CategorizeId",
                        column: x => x.CategorizeId,
                        principalTable: "categorizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_LinesId",
                table: "products",
                column: "LinesId");

            migrationBuilder.CreateIndex(
                name: "IX_categorizes_CompanyId",
                table: "categorizes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_companies_Code",
                table: "companies",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_lines_CategorizeId",
                table: "lines",
                column: "CategorizeId");

            migrationBuilder.CreateIndex(
                name: "IX_lines_Code",
                table: "lines",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_categorizes_companies_CompanyId",
                table: "categorizes",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dataImages_lines_LinesId",
                table: "dataImages",
                column: "LinesId",
                principalTable: "lines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_products_lines_LinesId",
                table: "products",
                column: "LinesId",
                principalTable: "lines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categorizes_companies_CompanyId",
                table: "categorizes");

            migrationBuilder.DropForeignKey(
                name: "FK_dataImages_lines_LinesId",
                table: "dataImages");

            migrationBuilder.DropForeignKey(
                name: "FK_products_lines_LinesId",
                table: "products");

            migrationBuilder.DropTable(
                name: "companies");

            migrationBuilder.DropTable(
                name: "lines");

            migrationBuilder.DropIndex(
                name: "IX_products_LinesId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_categorizes_CompanyId",
                table: "categorizes");

            migrationBuilder.DropColumn(
                name: "LinesId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "categorizes");

            migrationBuilder.RenameColumn(
                name: "LinesId",
                table: "dataImages",
                newName: "CategorizeId");

            migrationBuilder.RenameIndex(
                name: "IX_dataImages_LinesId",
                table: "dataImages",
                newName: "IX_dataImages_CategorizeId");

            migrationBuilder.AddColumn<Guid>(
                name: "CategorizeId",
                table: "products",
                type: "uniqueidentifier",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ClassifyId",
                table: "commodities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "classifies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<string>(type: "Char(5)", maxLength: 5, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classifies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_CategorizeId",
                table: "products",
                column: "CategorizeId");

            migrationBuilder.CreateIndex(
                name: "IX_commodities_ClassifyId",
                table: "commodities",
                column: "ClassifyId");

            migrationBuilder.CreateIndex(
                name: "IX_classifies_Code",
                table: "classifies",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_commodities_classifies_ClassifyId",
                table: "commodities",
                column: "ClassifyId",
                principalTable: "classifies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dataImages_categorizes_CategorizeId",
                table: "dataImages",
                column: "CategorizeId",
                principalTable: "categorizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_products_categorizes_CategorizeId",
                table: "products",
                column: "CategorizeId",
                principalTable: "categorizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
