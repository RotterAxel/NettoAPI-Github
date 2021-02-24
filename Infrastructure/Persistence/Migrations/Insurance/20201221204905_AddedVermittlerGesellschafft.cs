using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations.Insurance
{
    public partial class AddedVermittlerGesellschafft : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VermittlerGesellschafften",
                columns: table => new
                {
                    VermittlerId = table.Column<int>(nullable: false),
                    GesellschaftId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VermittlerGesellschafften", x => new { x.VermittlerId, x.GesellschaftId });
                    table.ForeignKey(
                        name: "FK_VermittlerGesellschafften_GesellschaftSet_GesellschaftId",
                        column: x => x.GesellschaftId,
                        principalTable: "GesellschaftSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VermittlerGesellschafften_Vermittler_VermittlerId",
                        column: x => x.VermittlerId,
                        principalTable: "Vermittler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VermittlerGesellschafften_GesellschaftId",
                table: "VermittlerGesellschafften",
                column: "GesellschaftId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VermittlerGesellschafften");
        }
    }
}
