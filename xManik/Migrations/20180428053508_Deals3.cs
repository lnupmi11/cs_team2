using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace xManik.Migrations
{
    public partial class Deals3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "Deals",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Deals");
        }
    }
}
