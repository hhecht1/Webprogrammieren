using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechChamp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dozenten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vorname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nachname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fachgebiet = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dozenten", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Räume",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bezeichnung = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Kapazität = table.Column<int>(type: "int", nullable: false),
                    Gebäude = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Räume", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teilnehmer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vorname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nachname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teilnehmer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kurse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Beschreibung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kursart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxTeilnehmer = table.Column<int>(type: "int", nullable: false, defaultValue: 20),
                    Wiederholung = table.Column<bool>(type: "bit", nullable: false),
                    RaumId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kurse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kurse_Räume_RaumId",
                        column: x => x.RaumId,
                        principalTable: "Räume",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KursDozenten",
                columns: table => new
                {
                    KursId = table.Column<int>(type: "int", nullable: false),
                    DozentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KursDozenten", x => new { x.KursId, x.DozentId });
                    table.ForeignKey(
                        name: "FK_KursDozenten_Dozenten_DozentId",
                        column: x => x.DozentId,
                        principalTable: "Dozenten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KursDozenten_Kurse_KursId",
                        column: x => x.KursId,
                        principalTable: "Kurse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KursTeilnehmer",
                columns: table => new
                {
                    KursId = table.Column<int>(type: "int", nullable: false),
                    TeilnehmerId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Anmeldedatum = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KursTeilnehmer", x => new { x.KursId, x.TeilnehmerId });
                    table.ForeignKey(
                        name: "FK_KursTeilnehmer_Kurse_KursId",
                        column: x => x.KursId,
                        principalTable: "Kurse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KursTeilnehmer_Teilnehmer_TeilnehmerId",
                        column: x => x.TeilnehmerId,
                        principalTable: "Teilnehmer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KursDozenten_DozentId",
                table: "KursDozenten",
                column: "DozentId");

            migrationBuilder.CreateIndex(
                name: "IX_Kurse_RaumId",
                table: "Kurse",
                column: "RaumId");

            migrationBuilder.CreateIndex(
                name: "IX_KursTeilnehmer_TeilnehmerId",
                table: "KursTeilnehmer",
                column: "TeilnehmerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KursDozenten");

            migrationBuilder.DropTable(
                name: "KursTeilnehmer");

            migrationBuilder.DropTable(
                name: "Dozenten");

            migrationBuilder.DropTable(
                name: "Kurse");

            migrationBuilder.DropTable(
                name: "Teilnehmer");

            migrationBuilder.DropTable(
                name: "Räume");
        }
    }
}
