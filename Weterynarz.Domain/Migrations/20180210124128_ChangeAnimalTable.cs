using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Weterynarz.Domain.Migrations
{
    public partial class ChangeAnimalTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AnimalDesc",
                table: "Animals",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AnimalDesc",
                table: "Animals",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
