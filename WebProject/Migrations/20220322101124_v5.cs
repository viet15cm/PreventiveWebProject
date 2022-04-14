using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProject.Migrations
{
    public partial class v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataImage_categorizes_CategorizeId",
                table: "DataImage");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_products_ProdtuctId",
                table: "ProductImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataImage",
                table: "DataImage");

            migrationBuilder.RenameTable(
                name: "ProductImages",
                newName: "productImages");

            migrationBuilder.RenameTable(
                name: "DataImage",
                newName: "dataImages");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_ProdtuctId",
                table: "productImages",
                newName: "IX_productImages_ProdtuctId");

            migrationBuilder.RenameIndex(
                name: "IX_DataImage_CategorizeId",
                table: "dataImages",
                newName: "IX_dataImages_CategorizeId");

            migrationBuilder.AddColumn<Guid>(
                name: "ClassifyId",
                table: "commodities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "dataImages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_productImages",
                table: "productImages",
                columns: new[] { "DataImageId", "ProdtuctId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_dataImages",
                table: "dataImages",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "classifies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classifies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_commodities_ClassifyId",
                table: "commodities",
                column: "ClassifyId");

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
                name: "FK_productImages_dataImages_DataImageId",
                table: "productImages",
                column: "DataImageId",
                principalTable: "dataImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productImages_products_ProdtuctId",
                table: "productImages",
                column: "ProdtuctId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_commodities_classifies_ClassifyId",
                table: "commodities");

            migrationBuilder.DropForeignKey(
                name: "FK_dataImages_categorizes_CategorizeId",
                table: "dataImages");

            migrationBuilder.DropForeignKey(
                name: "FK_productImages_dataImages_DataImageId",
                table: "productImages");

            migrationBuilder.DropForeignKey(
                name: "FK_productImages_products_ProdtuctId",
                table: "productImages");

            migrationBuilder.DropTable(
                name: "classifies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productImages",
                table: "productImages");

            migrationBuilder.DropIndex(
                name: "IX_commodities_ClassifyId",
                table: "commodities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dataImages",
                table: "dataImages");

            migrationBuilder.DropColumn(
                name: "ClassifyId",
                table: "commodities");

            migrationBuilder.RenameTable(
                name: "productImages",
                newName: "ProductImages");

            migrationBuilder.RenameTable(
                name: "dataImages",
                newName: "DataImage");

            migrationBuilder.RenameIndex(
                name: "IX_productImages_ProdtuctId",
                table: "ProductImages",
                newName: "IX_ProductImages_ProdtuctId");

            migrationBuilder.RenameIndex(
                name: "IX_dataImages_CategorizeId",
                table: "DataImage",
                newName: "IX_DataImage_CategorizeId");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "DataImage",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages",
                columns: new[] { "DataImageId", "ProdtuctId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataImage",
                table: "DataImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataImage_categorizes_CategorizeId",
                table: "DataImage",
                column: "CategorizeId",
                principalTable: "categorizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_products_ProdtuctId",
                table: "ProductImages",
                column: "ProdtuctId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
