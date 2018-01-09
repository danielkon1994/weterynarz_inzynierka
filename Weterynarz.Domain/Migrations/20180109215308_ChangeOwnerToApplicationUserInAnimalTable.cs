using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Weterynarz.Domain.Migrations
{
    public partial class ChangeOwnerToApplicationUserInAnimalTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_AspNetUsers_ApplicationUserId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Clients_OwnerId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_ApplicationUserId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_OwnerId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Animals");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Animals",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Animals",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_ClientId",
                table: "Animals",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Clients_ClientId",
                table: "Animals",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_AspNetUsers_OwnerId",
                table: "Animals",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Clients_ClientId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Animals_AspNetUsers_OwnerId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_ClientId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Animals");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Animals",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Animals",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_ApplicationUserId",
                table: "Animals",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_AspNetUsers_ApplicationUserId",
                table: "Animals",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Clients_OwnerId",
                table: "Animals",
                column: "OwnerId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
