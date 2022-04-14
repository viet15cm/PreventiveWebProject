using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProject.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_commodities_CommodityId",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "CommodityId",
                table: "products",
                newName: "CategorizeId");

            migrationBuilder.RenameIndex(
                name: "IX_products_CommodityId",
                table: "products",
                newName: "IX_products_CategorizeId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "commodities",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.CreateTable(
                name: "categorizes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Nane = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    ProdtuctId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => new { x.DataImageId, x.ProdtuctId });
                    table.ForeignKey(
                        name: "FK_ProductImages_products_ProdtuctId",
                        column: x => x.ProdtuctId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UrlImg = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CategorizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataImage_categorizes_CategorizeId",
                        column: x => x.CategorizeId,
                        principalTable: "categorizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataImage_CategorizeId",
                table: "DataImage",
                column: "CategorizeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProdtuctId",
                table: "ProductImages",
                column: "ProdtuctId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_categorizes_CategorizeId",
                table: "products",
                column: "CategorizeId",
                principalTable: "categorizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_categorizes_CategorizeId",
                table: "products");

            migrationBuilder.DropTable(
                name: "DataImage");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "categorizes");

            migrationBuilder.RenameColumn(
                name: "CategorizeId",
                table: "products",
                newName: "CommodityId");

            migrationBuilder.RenameIndex(
                name: "IX_products_CategorizeId",
                table: "products",
                newName: "IX_products_CommodityId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "commodities",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddForeignKey(
                name: "FK_products_commodities_CommodityId",
                table: "products",
                column: "CommodityId",
                principalTable: "commodities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
