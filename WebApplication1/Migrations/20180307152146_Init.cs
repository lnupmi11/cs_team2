using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace xManik.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Artworks",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Artworks",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "ProviderId",
                table: "Artworks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Artworks_ProviderId",
                table: "Artworks",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artworks_Providers_ProviderId",
                table: "Artworks",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artworks_Providers_ProviderId",
                table: "Artworks");

            migrationBuilder.DropIndex(
                name: "IX_Artworks_ProviderId",
                table: "Artworks");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "Artworks");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Artworks",
                newName: "ID");

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "Artworks",
                nullable: false,
                oldClrType: typeof(string))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        }
    }
}
