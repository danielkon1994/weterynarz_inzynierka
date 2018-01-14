using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Weterynarz.Domain.Migrations
{
    public partial class ChangeInVisitTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AnimalDescription",
                table: "Visits",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Visits",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Visits");

            migrationBuilder.AlterColumn<string>(
                name: "AnimalDescription",
                table: "Visits",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
