using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace xManik.Migrations
{
    public partial class Deals2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AssigmentId",
                table: "Deals",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deals_AssigmentId",
                table: "Deals",
                column: "AssigmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Assigments_AssigmentId",
                table: "Deals",
                column: "AssigmentId",
                principalTable: "Assigments",
                principalColumn: "AssigmentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Assigments_AssigmentId",
                table: "Deals");

            migrationBuilder.DropIndex(
                name: "IX_Deals_AssigmentId",
                table: "Deals");

            migrationBuilder.AlterColumn<string>(
                name: "AssigmentId",
                table: "Deals",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
