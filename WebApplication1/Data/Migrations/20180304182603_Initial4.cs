using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication1.Data.Migrations
{
    public partial class Initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientProperty",
                table: "LoggedIn",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "LoggedIn",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProviderProperty",
                table: "LoggedIn",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientProperty",
                table: "LoggedIn");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "LoggedIn");

            migrationBuilder.DropColumn(
                name: "ProviderProperty",
                table: "LoggedIn");
        }
    }
}
