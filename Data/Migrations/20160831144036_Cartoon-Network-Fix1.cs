using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cartoonalogue.Data.Migrations
{
    public partial class CartoonNetworkFix1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cartoons_Networks_NetworkId",
                table: "Cartoons");

            migrationBuilder.DropIndex(
                name: "IX_Cartoons_NetworkId",
                table: "Cartoons");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
