using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WerkstattPro.Migrations
{
    /// <inheritdoc />
    public partial class InitalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ersatzteile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bezeichnung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Artikelnummer = table.Column<int>(type: "int", nullable: false),
                    Lagerbestand = table.Column<int>(type: "int", nullable: false),
                    Preis = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ersatzteile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kunden",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vorname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nachname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefonnummer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kunden", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mechaniker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vorname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nachname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fachgebiet = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mechaniker", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fahrzeuge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kennzeichen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marke = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Baujahr = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KundeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fahrzeuge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fahrzeuge_Kunden_KundeId",
                        column: x => x.KundeId,
                        principalTable: "Kunden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReparaturAuftraege",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Beschreibung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuftragsDatum = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    FahrzeugId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReparaturAuftraege", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReparaturAuftraege_Fahrzeuge_FahrzeugId",
                        column: x => x.FahrzeugId,
                        principalTable: "Fahrzeuge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuftragErsatzteile",
                columns: table => new
                {
                    ReparaturAuftragId = table.Column<int>(type: "int", nullable: false),
                    ErsatzteilId = table.Column<int>(type: "int", nullable: false),
                    Menge = table.Column<int>(type: "int", nullable: false),
                    Einzlpreis = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuftragErsatzteile", x => new { x.ReparaturAuftragId, x.ErsatzteilId });
                    table.ForeignKey(
                        name: "FK_AuftragErsatzteile_Ersatzteile_ErsatzteilId",
                        column: x => x.ErsatzteilId,
                        principalTable: "Ersatzteile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuftragErsatzteile_ReparaturAuftraege_ReparaturAuftragId",
                        column: x => x.ReparaturAuftragId,
                        principalTable: "ReparaturAuftraege",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuftragMechaniker",
                columns: table => new
                {
                    ReparaturAuftragId = table.Column<int>(type: "int", nullable: false),
                    MechanikerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuftragMechaniker", x => new { x.ReparaturAuftragId, x.MechanikerId });
                    table.ForeignKey(
                        name: "FK_AuftragMechaniker_Mechaniker_MechanikerId",
                        column: x => x.MechanikerId,
                        principalTable: "Mechaniker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuftragMechaniker_ReparaturAuftraege_ReparaturAuftragId",
                        column: x => x.ReparaturAuftragId,
                        principalTable: "ReparaturAuftraege",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuftragErsatzteile_ErsatzteilId",
                table: "AuftragErsatzteile",
                column: "ErsatzteilId");

            migrationBuilder.CreateIndex(
                name: "IX_AuftragMechaniker_MechanikerId",
                table: "AuftragMechaniker",
                column: "MechanikerId");

            migrationBuilder.CreateIndex(
                name: "IX_Fahrzeuge_KundeId",
                table: "Fahrzeuge",
                column: "KundeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReparaturAuftraege_FahrzeugId",
                table: "ReparaturAuftraege",
                column: "FahrzeugId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuftragErsatzteile");

            migrationBuilder.DropTable(
                name: "AuftragMechaniker");

            migrationBuilder.DropTable(
                name: "Ersatzteile");

            migrationBuilder.DropTable(
                name: "Mechaniker");

            migrationBuilder.DropTable(
                name: "ReparaturAuftraege");

            migrationBuilder.DropTable(
                name: "Fahrzeuge");

            migrationBuilder.DropTable(
                name: "Kunden");
        }
    }
}
