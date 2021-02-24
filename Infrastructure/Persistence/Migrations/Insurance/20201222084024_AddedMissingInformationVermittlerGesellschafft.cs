using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations.Insurance
{
    public partial class AddedMissingInformationVermittlerGesellschafft : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Abschlussvergütung",
                table: "VermittlerGesellschafften",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Bestandsvergütung",
                table: "VermittlerGesellschafften",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "MaxLaufzeitVergütung",
                table: "VermittlerGesellschafften",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "VermittlerNo",
                table: "VermittlerGesellschafften",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abschlussvergütung",
                table: "VermittlerGesellschafften");

            migrationBuilder.DropColumn(
                name: "Bestandsvergütung",
                table: "VermittlerGesellschafften");

            migrationBuilder.DropColumn(
                name: "MaxLaufzeitVergütung",
                table: "VermittlerGesellschafften");

            migrationBuilder.DropColumn(
                name: "VermittlerNo",
                table: "VermittlerGesellschafften");
        }
    }
}
