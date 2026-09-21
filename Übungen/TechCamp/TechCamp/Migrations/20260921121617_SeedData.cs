using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechChamp.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Anmeldedatum",
                table: "KursTeilnehmer",
                newName: "AnmeldeDatum");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnmeldeDatum",
                table: "KursTeilnehmer",
                newName: "Anmeldedatum");
        }
    }
}
