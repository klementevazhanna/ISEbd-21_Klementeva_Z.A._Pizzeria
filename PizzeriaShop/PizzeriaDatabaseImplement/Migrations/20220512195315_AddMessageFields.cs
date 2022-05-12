using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzeriaDatabaseImplement.Migrations
{
    public partial class AddMessageFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasBeenRead",
                table: "Messages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Response",
                table: "Messages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasBeenRead",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Response",
                table: "Messages");
        }
    }
}
