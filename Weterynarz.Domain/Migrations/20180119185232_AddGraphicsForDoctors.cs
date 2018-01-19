using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Weterynarz.Domain.Migrations
{
    public partial class AddGraphicsForDoctors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Graphics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    DoctorGraphicId = table.Column<int>(nullable: false),
                    FridayFrom = table.Column<int>(nullable: false),
                    FridayTo = table.Column<int>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    MondayFrom = table.Column<int>(nullable: false),
                    MondayTo = table.Column<int>(nullable: false),
                    SaturdayFrom = table.Column<int>(nullable: false),
                    SaturdayTo = table.Column<int>(nullable: false),
                    SundayFrom = table.Column<int>(nullable: false),
                    SundayTo = table.Column<int>(nullable: false),
                    ThursdayFrom = table.Column<int>(nullable: false),
                    ThursdayTo = table.Column<int>(nullable: false),
                    TuesdayFrom = table.Column<int>(nullable: false),
                    TuesdayTo = table.Column<int>(nullable: false),
                    WednesdayFrom = table.Column<int>(nullable: false),
                    WednesdayTo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Graphics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoctorGraphics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    AvailableFrom = table.Column<DateTime>(nullable: false),
                    AvailableTo = table.Column<DateTime>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    DoctorId = table.Column<string>(nullable: false),
                    GraphicId = table.Column<int>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorGraphics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorGraphics_AspNetUsers_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorGraphics_Graphics_GraphicId",
                        column: x => x.GraphicId,
                        principalTable: "Graphics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorGraphics_DoctorId",
                table: "DoctorGraphics",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorGraphics_GraphicId",
                table: "DoctorGraphics",
                column: "GraphicId");

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
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorGraphics_Graphics_GraphicId",
                table: "DoctorGraphics");

            migrationBuilder.DropTable(
                name: "Graphics");

            migrationBuilder.DropTable(
                name: "DoctorGraphics");
        }
    }
}
