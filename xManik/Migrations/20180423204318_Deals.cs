using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace xManik.Migrations
{
    public partial class Deals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deals",
                columns: table => new
                {
                    DealId = table.Column<string>(nullable: false),
                    AssigmentId = table.Column<string>(nullable: true),
                    BloggerId = table.Column<string>(nullable: true),
                    ClientId = table.Column<string>(nullable: true),
                    IsRead = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deals", x => x.DealId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deals");
        }
    }
}
