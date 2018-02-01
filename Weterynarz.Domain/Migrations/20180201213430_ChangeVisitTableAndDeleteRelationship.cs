using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Weterynarz.Domain.Migrations
{
    public partial class ChangeVisitTableAndDeleteRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalExaminations");

            migrationBuilder.AddColumn<string>(
                name: "Diagnosis",
                table: "Visits",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VisitId",
                table: "MedicalExaminationTypes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VisitId",
                table: "Diseases",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalExaminationTypes_VisitId",
                table: "MedicalExaminationTypes",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_VisitId",
                table: "Diseases",
                column: "VisitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diseases_Visits_VisitId",
                table: "Diseases",
                column: "VisitId",
                principalTable: "Visits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalExaminationTypes_Visits_VisitId",
                table: "MedicalExaminationTypes",
                column: "VisitId",
                principalTable: "Visits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diseases_Visits_VisitId",
                table: "Diseases");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalExaminationTypes_Visits_VisitId",
                table: "MedicalExaminationTypes");

            migrationBuilder.DropIndex(
                name: "IX_MedicalExaminationTypes_VisitId",
                table: "MedicalExaminationTypes");

            migrationBuilder.DropIndex(
                name: "IX_Diseases_VisitId",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "Diagnosis",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "VisitId",
                table: "MedicalExaminationTypes");

            migrationBuilder.DropColumn(
                name: "VisitId",
                table: "Diseases");

            migrationBuilder.CreateTable(
                name: "MedicalExaminations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    AnimalId = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DoctorId = table.Column<string>(nullable: true),
                    ExaminationDate = table.Column<DateTime>(nullable: false),
                    MedicalExaminationTypeId = table.Column<int>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalExaminations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalExaminations_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalExaminations_AspNetUsers_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalExaminations_MedicalExaminationTypes_MedicalExaminationTypeId",
                        column: x => x.MedicalExaminationTypeId,
                        principalTable: "MedicalExaminationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalExaminations_AnimalId",
                table: "MedicalExaminations",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalExaminations_DoctorId",
                table: "MedicalExaminations",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalExaminations_MedicalExaminationTypeId",
                table: "MedicalExaminations",
                column: "MedicalExaminationTypeId");
        }
    }
}
