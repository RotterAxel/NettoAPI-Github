using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations.Insurance
{
    public partial class FirmenRemovedFromDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dokumente_Firmen_FirmaRegistrierungsDokumentHistorienId",
                table: "Dokumente");

            migrationBuilder.DropForeignKey(
                name: "FK_Dokumente_Firmen_FirmaRegistrierungsDokumentId",
                table: "Dokumente");

            migrationBuilder.DropForeignKey(
                name: "FK_Vermittler_Firmen_FirmaIdBevollmächtigterVermittler",
                table: "Vermittler");

            migrationBuilder.DropForeignKey(
                name: "FK_Vermittler_Firmen_MitarbeiterVonFirmaId",
                table: "Vermittler");

            migrationBuilder.DropTable(
                name: "Firmen");

            migrationBuilder.DropIndex(
                name: "IX_Vermittler_FirmaIdBevollmächtigterVermittler",
                table: "Vermittler");

            migrationBuilder.DropIndex(
                name: "IX_Vermittler_MitarbeiterVonFirmaId",
                table: "Vermittler");

            migrationBuilder.DropIndex(
                name: "IX_Dokumente_FirmaRegistrierungsDokumentHistorienId",
                table: "Dokumente");

            migrationBuilder.DropIndex(
                name: "IX_Dokumente_FirmaRegistrierungsDokumentId",
                table: "Dokumente");

            migrationBuilder.DropColumn(
                name: "FirmaIdBevollmächtigterVermittler",
                table: "Vermittler");

            migrationBuilder.DropColumn(
                name: "MitarbeiterVonFirmaId",
                table: "Vermittler");

            migrationBuilder.DropColumn(
                name: "FirmaRegistrierungsDokumentHistorienId",
                table: "Dokumente");

            migrationBuilder.DropColumn(
                name: "FirmaRegistrierungsDokumentId",
                table: "Dokumente");

            migrationBuilder.DropColumn(
                name: "IstFirmenDokumentArt",
                table: "DokumentArtSet");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FirmaIdBevollmächtigterVermittler",
                table: "Vermittler",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MitarbeiterVonFirmaId",
                table: "Vermittler",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FirmaRegistrierungsDokumentHistorienId",
                table: "Dokumente",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FirmaRegistrierungsDokumentId",
                table: "Dokumente",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IstFirmenDokumentArt",
                table: "DokumentArtSet",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Firmen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AdresseId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Firmenname = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    ModifiedBy = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    RowVersion = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firmen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Firmen_Adressen_AdresseId",
                        column: x => x.AdresseId,
                        principalTable: "Adressen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vermittler_FirmaIdBevollmächtigterVermittler",
                table: "Vermittler",
                column: "FirmaIdBevollmächtigterVermittler",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vermittler_MitarbeiterVonFirmaId",
                table: "Vermittler",
                column: "MitarbeiterVonFirmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Dokumente_FirmaRegistrierungsDokumentHistorienId",
                table: "Dokumente",
                column: "FirmaRegistrierungsDokumentHistorienId");

            migrationBuilder.CreateIndex(
                name: "IX_Dokumente_FirmaRegistrierungsDokumentId",
                table: "Dokumente",
                column: "FirmaRegistrierungsDokumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Firmen_AdresseId",
                table: "Firmen",
                column: "AdresseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dokumente_Firmen_FirmaRegistrierungsDokumentHistorienId",
                table: "Dokumente",
                column: "FirmaRegistrierungsDokumentHistorienId",
                principalTable: "Firmen",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Dokumente_Firmen_FirmaRegistrierungsDokumentId",
                table: "Dokumente",
                column: "FirmaRegistrierungsDokumentId",
                principalTable: "Firmen",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Vermittler_Firmen_FirmaIdBevollmächtigterVermittler",
                table: "Vermittler",
                column: "FirmaIdBevollmächtigterVermittler",
                principalTable: "Firmen",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Vermittler_Firmen_MitarbeiterVonFirmaId",
                table: "Vermittler",
                column: "MitarbeiterVonFirmaId",
                principalTable: "Firmen",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
