using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_supplier",
                columns: table => new
                {
                    tsu__id = table.Column<string>(nullable: false),
                    tsu__name = table.Column<string>(nullable: false),
                    tsu__code = table.Column<string>(nullable: false),
                    tsu_cnpj = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_supplier", x => x.tsu__id);
                });

            migrationBuilder.CreateTable(
                name: "tb_type_asset",
                columns: table => new
                {
                    tta__id = table.Column<string>(nullable: false),
                    tta__name = table.Column<string>(nullable: false),
                    tta__code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_type_asset", x => x.tta__id);
                });

            migrationBuilder.CreateTable(
                name: "tb_order",
                columns: table => new
                {
                    tor__id = table.Column<string>(nullable: false),
                    tor__contract_number = table.Column<string>(nullable: false),
                    tor__created_at = table.Column<DateTime>(nullable: false),
                    tor__updated_at = table.Column<DateTime>(nullable: false),
                    tor__supplier_id = table.Column<string>(nullable: false),
                    tor__discount = table.Column<decimal>(type: "Money", nullable: false, defaultValue: 0m),
                    tor__total = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_order", x => x.tor__id);
                    table.ForeignKey(
                        name: "FK_tb_order_tb_supplier_tor__supplier_id",
                        column: x => x.tor__supplier_id,
                        principalTable: "tb_supplier",
                        principalColumn: "tsu__id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_asset",
                columns: table => new
                {
                    tas__id = table.Column<string>(nullable: false),
                    tas__name = table.Column<string>(nullable: false),
                    tas__code = table.Column<string>(nullable: false),
                    tas__type_asset_id = table.Column<string>(nullable: false),
                    tas__price = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_asset", x => x.tas__id);
                    table.ForeignKey(
                        name: "FK_tb_asset_tb_type_asset_tas__type_asset_id",
                        column: x => x.tas__type_asset_id,
                        principalTable: "tb_type_asset",
                        principalColumn: "tta__id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_item_order",
                columns: table => new
                {
                    tio__id = table.Column<string>(nullable: false),
                    tio__asset_id = table.Column<string>(nullable: false),
                    tio__quantity = table.Column<int>(nullable: false),
                    tio__unit_price = table.Column<decimal>(type: "Money", nullable: false),
                    tio__order_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_item_order", x => x.tio__id);
                    table.ForeignKey(
                        name: "FK_tb_item_order_tb_asset_tio__asset_id",
                        column: x => x.tio__asset_id,
                        principalTable: "tb_asset",
                        principalColumn: "tas__id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_item_order_tb_order_tio__order_id",
                        column: x => x.tio__order_id,
                        principalTable: "tb_order",
                        principalColumn: "tor__id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_asset_tas__type_asset_id",
                table: "tb_asset",
                column: "tas__type_asset_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_item_order_tio__asset_id",
                table: "tb_item_order",
                column: "tio__asset_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_item_order_tio__order_id",
                table: "tb_item_order",
                column: "tio__order_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_order_tor__supplier_id",
                table: "tb_order",
                column: "tor__supplier_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_item_order");

            migrationBuilder.DropTable(
                name: "tb_asset");

            migrationBuilder.DropTable(
                name: "tb_order");

            migrationBuilder.DropTable(
                name: "tb_type_asset");

            migrationBuilder.DropTable(
                name: "tb_supplier");
        }
    }
}
