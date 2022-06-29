using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Rise.PhoneDirectory.Repository.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Directory");

            migrationBuilder.CreateTable(
                name: "Persons",
                schema: "Directory",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Surname = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    CompanyName = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                schema: "Directory",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RequestTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ReportStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ReportId);
                });

            migrationBuilder.CreateTable(
                name: "ContactInformations",
                schema: "Directory",
                columns: table => new
                {
                    ContactInformationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InformationType = table.Column<int>(type: "integer", nullable: false),
                    InformationContent = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false),
                    PersonId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInformations", x => x.ContactInformationId);
                    table.ForeignKey(
                        name: "FK_ContactInformations_Persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "Directory",
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Directory",
                table: "Persons",
                columns: new[] { "PersonId", "CompanyName", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, "What You Will Yoga Inc.", "Carol", "Austin" },
                    { 2, "Top It Off Inc.", "Bella", "Burgess" },
                    { 3, "Soft As a Grape Inc.", "Diana", "Edmunds" },
                    { 4, "Saga Innovations", "Emma", "King" }
                });

            migrationBuilder.InsertData(
                schema: "Directory",
                table: "ContactInformations",
                columns: new[] { "ContactInformationId", "InformationContent", "InformationType", "PersonId" },
                values: new object[,]
                {
                    { 1, "carol@yoga.com", 1, 1 },
                    { 2, "1-541-754-3010", 0, 1 },
                    { 3, "Washington", 2, 1 },
                    { 4, "bella@topitoff.com", 1, 2 },
                    { 5, "1-541-452-2180", 0, 2 },
                    { 6, "Washington", 2, 2 },
                    { 7, "diana@grape.com", 1, 3 },
                    { 8, "1-852-142-1149", 0, 3 },
                    { 9, "California", 2, 3 },
                    { 10, "emma@sagainnovations.com", 1, 4 },
                    { 11, "1-852-854-6310", 0, 4 },
                    { 12, "California", 2, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactInformations_InformationType",
                schema: "Directory",
                table: "ContactInformations",
                column: "InformationType");

            migrationBuilder.CreateIndex(
                name: "IX_ContactInformations_PersonId_InformationType_InformationCon~",
                schema: "Directory",
                table: "ContactInformations",
                columns: new[] { "PersonId", "InformationType", "InformationContent" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Name_Surname",
                schema: "Directory",
                table: "Persons",
                columns: new[] { "Name", "Surname" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactInformations",
                schema: "Directory");

            migrationBuilder.DropTable(
                name: "Reports",
                schema: "Directory");

            migrationBuilder.DropTable(
                name: "Persons",
                schema: "Directory");
        }
    }
}
