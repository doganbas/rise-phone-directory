using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rise.PhoneDirectory.Repository.Migrations
{
    public partial class updateReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                schema: "Directory",
                table: "Reports",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                schema: "Directory",
                table: "Reports",
                type: "character varying(400)",
                maxLength: 400,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedTime",
                schema: "Directory",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "FilePath",
                schema: "Directory",
                table: "Reports");
        }
    }
}
