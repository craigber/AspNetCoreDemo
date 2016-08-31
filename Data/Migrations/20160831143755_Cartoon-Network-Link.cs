using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreDemo2.Data.Migrations
{
    public partial class CartoonNetworkLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Network",
                table: "Cartoons");

            migrationBuilder.CreateIndex(
                name: "IX_Cartoons_NetworkId",
                table: "Cartoons",
                column: "NetworkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cartoons_Networks_NetworkId",
                table: "Cartoons",
                column: "NetworkId",
                principalTable: "Networks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cartoons_Networks_NetworkId",
                table: "Cartoons");

            migrationBuilder.DropIndex(
                name: "IX_Cartoons_NetworkId",
                table: "Cartoons");

            migrationBuilder.AddColumn<string>(
                name: "Network",
                table: "Cartoons",
                nullable: true);
        }
    }
}
