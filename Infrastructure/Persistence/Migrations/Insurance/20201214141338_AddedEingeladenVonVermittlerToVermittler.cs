using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations.Insurance
{
    public partial class AddedEingeladenVonVermittlerToVermittler : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kunde_Vermittler_VermittlerId",
                table: "Kunde");

            migrationBuilder.AddColumn<int>(
                name: "EingeladenVonVermittlerId",
                table: "Vermittler",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vermittler_EingeladenVonVermittlerId",
                table: "Vermittler",
                column: "EingeladenVonVermittlerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Kunde_Vermittler_VermittlerId",
                table: "Kunde",
                column: "VermittlerId",
                principalTable: "Vermittler",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Vermittler_Vermittler_EingeladenVonVermittlerId",
                table: "Vermittler",
                column: "EingeladenVonVermittlerId",
                principalTable: "Vermittler",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kunde_Vermittler_VermittlerId",
                table: "Kunde");

            migrationBuilder.DropForeignKey(
                name: "FK_Vermittler_Vermittler_EingeladenVonVermittlerId",
                table: "Vermittler");

            migrationBuilder.DropIndex(
                name: "IX_Vermittler_EingeladenVonVermittlerId",
                table: "Vermittler");

            migrationBuilder.DropColumn(
                name: "EingeladenVonVermittlerId",
                table: "Vermittler");

            migrationBuilder.AddForeignKey(
                name: "FK_Kunde_Vermittler_VermittlerId",
                table: "Kunde",
                column: "VermittlerId",
                principalTable: "Vermittler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
