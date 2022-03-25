using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzeriaDatabaseImplement.Migrations
{
    public partial class InitialWareHouses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WareHouses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WareHouseName = table.Column<string>(nullable: false),
                    ResponsiblePersonFIO = table.Column<string>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WareHouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WareHouseIngredients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WareHouseId = table.Column<int>(nullable: false),
                    IngredientId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WareHouseIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WareHouseIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WareHouseIngredients_WareHouses_WareHouseId",
                        column: x => x.WareHouseId,
                        principalTable: "WareHouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WareHouseIngredients_IngredientId",
                table: "WareHouseIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_WareHouseIngredients_WareHouseId",
                table: "WareHouseIngredients",
                column: "WareHouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WareHouseIngredients");

            migrationBuilder.DropTable(
                name: "WareHouses");
        }
    }
}
