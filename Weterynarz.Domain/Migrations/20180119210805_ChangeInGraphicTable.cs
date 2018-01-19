using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Weterynarz.Domain.Migrations
{
    public partial class ChangeInGraphicTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Graphics_DoctorGraphics_DoctorGraphicId",
                table: "Graphics");

            migrationBuilder.DropIndex(
                name: "IX_Graphics_DoctorGraphicId",
                table: "Graphics");

            migrationBuilder.DropColumn(
                name: "DoctorGraphicId",
                table: "Graphics");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorGraphicId",
                table: "Graphics",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Graphics_DoctorGraphicId",
                table: "Graphics",
                column: "DoctorGraphicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Graphics_DoctorGraphics_DoctorGraphicId",
                table: "Graphics",
                column: "DoctorGraphicId",
                principalTable: "DoctorGraphics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
