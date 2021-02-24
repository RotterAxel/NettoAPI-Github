using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations.Insurance
{
    public partial class EinladecodesToEinladecodesVermittler : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Einladecodes_Vermittler_VermittlerId",
                table: "Einladecodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Vermittler_Einladecodes_EingeladenVonVermittlerEinladecodeId",
                table: "Vermittler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Einladecodes",
                table: "Einladecodes");

            migrationBuilder.RenameTable(
                name: "Einladecodes",
                newName: "EinladecodesVermittler");

            migrationBuilder.RenameIndex(
                name: "IX_Einladecodes_VermittlerId",
                table: "EinladecodesVermittler",
                newName: "IX_EinladecodesVermittler_VermittlerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EinladecodesVermittler",
                table: "EinladecodesVermittler",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EinladecodesVermittler_Vermittler_VermittlerId",
                table: "EinladecodesVermittler",
                column: "VermittlerId",
                principalTable: "Vermittler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vermittler_EinladecodesVermittler_EingeladenVonVermittlerEin~",
                table: "Vermittler",
                column: "EingeladenVonVermittlerEinladecodeId",
                principalTable: "EinladecodesVermittler",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EinladecodesVermittler_Vermittler_VermittlerId",
                table: "EinladecodesVermittler");

            migrationBuilder.DropForeignKey(
                name: "FK_Vermittler_EinladecodesVermittler_EingeladenVonVermittlerEin~",
                table: "Vermittler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EinladecodesVermittler",
                table: "EinladecodesVermittler");

            migrationBuilder.RenameTable(
                name: "EinladecodesVermittler",
                newName: "Einladecodes");

            migrationBuilder.RenameIndex(
                name: "IX_EinladecodesVermittler_VermittlerId",
                table: "Einladecodes",
                newName: "IX_Einladecodes_VermittlerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Einladecodes",
                table: "Einladecodes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Einladecodes_Vermittler_VermittlerId",
                table: "Einladecodes",
                column: "VermittlerId",
                principalTable: "Vermittler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vermittler_Einladecodes_EingeladenVonVermittlerEinladecodeId",
                table: "Vermittler",
                column: "EingeladenVonVermittlerEinladecodeId",
                principalTable: "Einladecodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
