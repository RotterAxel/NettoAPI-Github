using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations.Insurance
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bankverbindungen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    Kontoinhaber = table.Column<string>(nullable: true),
                    IBAN = table.Column<string>(nullable: false),
                    BankName = table.Column<string>(nullable: false),
                    BIC = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bankverbindungen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Berufe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Berufe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DokumentArtSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    Name = table.Column<string>(nullable: false),
                    IstFirmenDokumentArt = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DokumentArtSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GesellschaftSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GesellschaftSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Länder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Länder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TitelSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    BezeichnungKurz = table.Column<string>(nullable: false),
                    Beschreibung = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitelSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Adressen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    Straße = table.Column<string>(nullable: false),
                    Hausnummer = table.Column<string>(nullable: false),
                    Plz = table.Column<string>(nullable: false),
                    Ort = table.Column<string>(nullable: false),
                    LandId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adressen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adressen_Länder_LandId",
                        column: x => x.LandId,
                        principalTable: "Länder",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ausweise",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    Behörde = table.Column<string>(nullable: false),
                    Geburtsname = table.Column<string>(nullable: true),
                    Ausstellungsdatum = table.Column<DateTime>(nullable: false),
                    Aublaufdatum = table.Column<DateTime>(nullable: false),
                    Ausweisnummer = table.Column<string>(nullable: false),
                    Geburtsort = table.Column<string>(nullable: false),
                    StaatsangehörigkeitId = table.Column<int>(nullable: false),
                    Ausweisart = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ausweise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ausweise_Länder_StaatsangehörigkeitId",
                        column: x => x.StaatsangehörigkeitId,
                        principalTable: "Länder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Firmen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    Firmenname = table.Column<string>(nullable: false),
                    AdresseId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    KeycloakIdentifier = table.Column<Guid>(nullable: false),
                    EMail = table.Column<string>(nullable: false),
                    Vorname = table.Column<string>(nullable: false),
                    Nachname = table.Column<string>(nullable: false),
                    Telefon = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    Geburtsdatum = table.Column<DateTime>(nullable: true),
                    Geburtsort = table.Column<string>(nullable: true),
                    StaatsangehörigkeitId = table.Column<int>(nullable: true),
                    Anrede = table.Column<int>(nullable: false),
                    AdresseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Adressen_AdresseId",
                        column: x => x.AdresseId,
                        principalTable: "Adressen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Länder_StaatsangehörigkeitId",
                        column: x => x.StaatsangehörigkeitId,
                        principalTable: "Länder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vermittler",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    BestandsProvisionssatz = table.Column<float>(nullable: false),
                    AbschlussProvisionssatz = table.Column<float>(nullable: false),
                    IstAktiv = table.Column<bool>(nullable: false),
                    IhkRegistrierungsnummer = table.Column<string>(nullable: false),
                    VermittlerRegistrierungsstatus = table.Column<int>(nullable: false),
                    FirmaIdBevollmächtigterVermittler = table.Column<int>(nullable: true),
                    MitarbeiterVonFirmaId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    BankverbindungId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vermittler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vermittler_Bankverbindungen_BankverbindungId",
                        column: x => x.BankverbindungId,
                        principalTable: "Bankverbindungen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vermittler_Firmen_FirmaIdBevollmächtigterVermittler",
                        column: x => x.FirmaIdBevollmächtigterVermittler,
                        principalTable: "Firmen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Vermittler_Firmen_MitarbeiterVonFirmaId",
                        column: x => x.MitarbeiterVonFirmaId,
                        principalTable: "Firmen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Vermittler_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dokumente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    Name = table.Column<string>(nullable: true),
                    DokumentenArtId = table.Column<int>(nullable: true),
                    Bearbeitungsstatus = table.Column<int>(nullable: false),
                    FileExtension = table.Column<int>(nullable: false),
                    Data = table.Column<byte[]>(type: "MediumBlob", nullable: false),
                    VermittlerRegistrierungsDokumentId = table.Column<int>(nullable: true),
                    VermittlerRegistrierungsDokumentHistorienId = table.Column<int>(nullable: true),
                    FirmaRegistrierungsDokumentId = table.Column<int>(nullable: true),
                    FirmaRegistrierungsDokumentHistorienId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dokumente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dokumente_DokumentArtSet_DokumentenArtId",
                        column: x => x.DokumentenArtId,
                        principalTable: "DokumentArtSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Dokumente_Firmen_FirmaRegistrierungsDokumentHistorienId",
                        column: x => x.FirmaRegistrierungsDokumentHistorienId,
                        principalTable: "Firmen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Dokumente_Firmen_FirmaRegistrierungsDokumentId",
                        column: x => x.FirmaRegistrierungsDokumentId,
                        principalTable: "Firmen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Dokumente_Vermittler_VermittlerRegistrierungsDokumentHistori~",
                        column: x => x.VermittlerRegistrierungsDokumentHistorienId,
                        principalTable: "Vermittler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Dokumente_Vermittler_VermittlerRegistrierungsDokumentId",
                        column: x => x.VermittlerRegistrierungsDokumentId,
                        principalTable: "Vermittler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Kunde",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    SteuerId = table.Column<string>(nullable: true),
                    ÖffentlichesAmt = table.Column<bool>(nullable: false),
                    IstVersicherungsnehmer = table.Column<bool>(nullable: false),
                    VersichertePerson = table.Column<bool>(nullable: false),
                    Familienstand = table.Column<int>(nullable: false),
                    Berufsstatus = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    TitelId = table.Column<int>(nullable: true),
                    BerufId = table.Column<int>(nullable: true),
                    AusweisId = table.Column<int>(nullable: true),
                    VermittlerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kunde", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kunde_Ausweise_AusweisId",
                        column: x => x.AusweisId,
                        principalTable: "Ausweise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Kunde_Berufe_BerufId",
                        column: x => x.BerufId,
                        principalTable: "Berufe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Kunde_TitelSet_TitelId",
                        column: x => x.TitelId,
                        principalTable: "TitelSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Kunde_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Kunde_Vermittler_VermittlerId",
                        column: x => x.VermittlerId,
                        principalTable: "Vermittler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProvisionssätzeGesellschaft",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false),
                    MaxLaufzeitInJahren = table.Column<int>(nullable: false),
                    VermittlerNr = table.Column<string>(nullable: true),
                    AbschlussVergütungProzent = table.Column<double>(nullable: false),
                    BestandsVergütungProzent = table.Column<double>(nullable: false),
                    VermittlerId = table.Column<int>(nullable: false),
                    GesellschaftId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvisionssätzeGesellschaft", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProvisionssätzeGesellschaft_GesellschaftSet_GesellschaftId",
                        column: x => x.GesellschaftId,
                        principalTable: "GesellschaftSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProvisionssätzeGesellschaft_Vermittler_VermittlerId",
                        column: x => x.VermittlerId,
                        principalTable: "Vermittler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adressen_LandId",
                table: "Adressen",
                column: "LandId");

            migrationBuilder.CreateIndex(
                name: "IX_Ausweise_StaatsangehörigkeitId",
                table: "Ausweise",
                column: "StaatsangehörigkeitId");

            migrationBuilder.CreateIndex(
                name: "IX_Dokumente_DokumentenArtId",
                table: "Dokumente",
                column: "DokumentenArtId");

            migrationBuilder.CreateIndex(
                name: "IX_Dokumente_FirmaRegistrierungsDokumentHistorienId",
                table: "Dokumente",
                column: "FirmaRegistrierungsDokumentHistorienId");

            migrationBuilder.CreateIndex(
                name: "IX_Dokumente_FirmaRegistrierungsDokumentId",
                table: "Dokumente",
                column: "FirmaRegistrierungsDokumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Dokumente_VermittlerRegistrierungsDokumentHistorienId",
                table: "Dokumente",
                column: "VermittlerRegistrierungsDokumentHistorienId");

            migrationBuilder.CreateIndex(
                name: "IX_Dokumente_VermittlerRegistrierungsDokumentId",
                table: "Dokumente",
                column: "VermittlerRegistrierungsDokumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Firmen_AdresseId",
                table: "Firmen",
                column: "AdresseId");

            migrationBuilder.CreateIndex(
                name: "IX_Kunde_AusweisId",
                table: "Kunde",
                column: "AusweisId");

            migrationBuilder.CreateIndex(
                name: "IX_Kunde_BerufId",
                table: "Kunde",
                column: "BerufId");

            migrationBuilder.CreateIndex(
                name: "IX_Kunde_TitelId",
                table: "Kunde",
                column: "TitelId");

            migrationBuilder.CreateIndex(
                name: "IX_Kunde_UserId",
                table: "Kunde",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kunde_VermittlerId",
                table: "Kunde",
                column: "VermittlerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvisionssätzeGesellschaft_GesellschaftId",
                table: "ProvisionssätzeGesellschaft",
                column: "GesellschaftId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvisionssätzeGesellschaft_VermittlerId",
                table: "ProvisionssätzeGesellschaft",
                column: "VermittlerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AdresseId",
                table: "Users",
                column: "AdresseId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StaatsangehörigkeitId",
                table: "Users",
                column: "StaatsangehörigkeitId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vermittler_BankverbindungId",
                table: "Vermittler",
                column: "BankverbindungId");

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
                name: "IX_Vermittler_UserId",
                table: "Vermittler",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dokumente");

            migrationBuilder.DropTable(
                name: "Kunde");

            migrationBuilder.DropTable(
                name: "ProvisionssätzeGesellschaft");

            migrationBuilder.DropTable(
                name: "DokumentArtSet");

            migrationBuilder.DropTable(
                name: "Ausweise");

            migrationBuilder.DropTable(
                name: "Berufe");

            migrationBuilder.DropTable(
                name: "TitelSet");

            migrationBuilder.DropTable(
                name: "GesellschaftSet");

            migrationBuilder.DropTable(
                name: "Vermittler");

            migrationBuilder.DropTable(
                name: "Bankverbindungen");

            migrationBuilder.DropTable(
                name: "Firmen");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Adressen");

            migrationBuilder.DropTable(
                name: "Länder");
        }
    }
}
