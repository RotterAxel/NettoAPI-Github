using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations.Insurance
{
    public partial class VermittlerEingeladenVonReferencedEinladecodeUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vermittler_Vermittler_EingeladenVonVermittlerId",
                table: "Vermittler");

            migrationBuilder.DropIndex(
                name: "IX_Vermittler_EingeladenVonVermittlerId",
                table: "Vermittler");

            migrationBuilder.DropColumn(
                name: "EingeladenVonVermittlerId",
                table: "Vermittler");

            migrationBuilder.AddColumn<int>(
                name: "EingeladenVonVermittlerEinladecodeId",
                table: "Vermittler",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vermittler_EingeladenVonVermittlerEinladecodeId",
                table: "Vermittler",
                column: "EingeladenVonVermittlerEinladecodeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vermittler_Einladecodes_EingeladenVonVermittlerEinladecodeId",
                table: "Vermittler",
                column: "EingeladenVonVermittlerEinladecodeId",
                principalTable: "Einladecodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vermittler_Einladecodes_EingeladenVonVermittlerEinladecodeId",
                table: "Vermittler");

            migrationBuilder.DropIndex(
                name: "IX_Vermittler_EingeladenVonVermittlerEinladecodeId",
                table: "Vermittler");

            migrationBuilder.DropColumn(
                name: "EingeladenVonVermittlerEinladecodeId",
                table: "Vermittler");

            migrationBuilder.AddColumn<int>(
                name: "EingeladenVonVermittlerId",
                table: "Vermittler",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vermittler_EingeladenVonVermittlerId",
                table: "Vermittler",
                column: "EingeladenVonVermittlerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vermittler_Vermittler_EingeladenVonVermittlerId",
                table: "Vermittler",
                column: "EingeladenVonVermittlerId",
                principalTable: "Vermittler",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
