using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Weterynarz.Domain.Migrations
{
    public partial class AddPriceToSummaryVisitTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "SummaryVisits",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "PriceSummaryVisits",
                columns: table => new
                {
                    SummaryVisitId = table.Column<int>(nullable: false),
                    PriceListId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceSummaryVisits", x => new { x.SummaryVisitId, x.PriceListId });
                    table.ForeignKey(
                        name: "FK_PriceSummaryVisits_PriceLists_PriceListId",
                        column: x => x.PriceListId,
                        principalTable: "PriceLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriceSummaryVisits_SummaryVisits_SummaryVisitId",
                        column: x => x.SummaryVisitId,
                        principalTable: "SummaryVisits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceSummaryVisits_PriceListId",
                table: "PriceSummaryVisits",
                column: "PriceListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceSummaryVisits");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "SummaryVisits");
        }
    }
}
