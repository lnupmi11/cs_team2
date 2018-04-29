using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace xManik.Migrations
{
    public partial class Deals4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsRead",
                table: "Deals",
                newName: "IsReadByClient");

            migrationBuilder.AddColumn<bool>(
                name: "IsReadByBlogger",
                table: "Deals",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReadByBlogger",
                table: "Deals");

            migrationBuilder.RenameColumn(
                name: "IsReadByClient",
                table: "Deals",
                newName: "IsRead");
        }
    }
}
