using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Weterynarz.Domain.Migrations
{
    public partial class ChangeVisitTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnimalDescription",
                table: "Visits",
                newName: "ReasonVisit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReasonVisit",
                table: "Visits",
                newName: "AnimalDescription");
        }
    }
}
