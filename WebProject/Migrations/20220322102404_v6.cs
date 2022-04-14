using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProject.Migrations
{
    public partial class v6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CommotityId",
                table: "categorizes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_categorizes_CommotityId",
                table: "categorizes",
                column: "CommotityId");

            migrationBuilder.AddForeignKey(
                name: "FK_categorizes_commodities_CommotityId",
                table: "categorizes",
                column: "CommotityId",
                principalTable: "commodities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categorizes_commodities_CommotityId",
                table: "categorizes");

            migrationBuilder.DropIndex(
                name: "IX_categorizes_CommotityId",
                table: "categorizes");

            migrationBuilder.DropColumn(
                name: "CommotityId",
                table: "categorizes");
        }
    }
}
