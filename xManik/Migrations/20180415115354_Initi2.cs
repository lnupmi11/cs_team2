using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace xManik.Migrations
{
    public partial class Initi2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloggerId",
                table: "Chanels");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Assigments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BloggerId",
                table: "Chanels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Assigments",
                nullable: true);
        }
    }
}
