using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations.Insurance
{
    public partial class AddedLaufzeitUndVergütungZuGesellschafft : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaximaleLaufzeitInJahren",
                table: "GesellschaftSet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "MaximaleVergütung",
                table: "GesellschaftSet",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "MinimaleLaufzeitInJahren",
                table: "GesellschaftSet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "MinimaleVergütung",
                table: "GesellschaftSet",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaximaleLaufzeitInJahren",
                table: "GesellschaftSet");

            migrationBuilder.DropColumn(
                name: "MaximaleVergütung",
                table: "GesellschaftSet");

            migrationBuilder.DropColumn(
                name: "MinimaleLaufzeitInJahren",
                table: "GesellschaftSet");

            migrationBuilder.DropColumn(
                name: "MinimaleVergütung",
                table: "GesellschaftSet");
        }
    }
}
