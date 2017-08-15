using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentLodge.Migrations
{
    public partial class DeleteCountryColumnFromAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apatrtment",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Addresses");

            migrationBuilder.AddColumn<string>(
                name: "ApartmentNumber",
                table: "Addresses",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CountryId",
                table: "Addresses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryId",
                table: "Addresses",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Countries_CountryId",
                table: "Addresses",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Countries_CountryId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CountryId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "ApartmentNumber",
                table: "Addresses");

            migrationBuilder.AddColumn<string>(
                name: "Apatrtment",
                table: "Addresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Addresses",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CountryId",
                table: "Addresses",
                nullable: true);
        }
    }
}
