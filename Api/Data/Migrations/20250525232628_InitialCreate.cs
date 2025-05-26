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
                    tsu_id = table.Column<string>(nullable: false),
                    tsu_name = table.Column<string>(nullable: false),
                    tsu_code = table.Column<string>(nullable: false),
                    tsu_cnpj = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_supplier", x => x.tsu_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_type_asset",
                columns: table => new
                {
                    tta_id = table.Column<string>(nullable: false),
                    tta_name = table.Column<string>(nullable: false),
                    tta_code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_type_asset", x => x.tta_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_order",
                columns: table => new
                {
                    tor_id = table.Column<string>(nullable: false),
                    tor_contract_number = table.Column<string>(nullable: false),
                    tor_created_at = table.Column<DateTime>(nullable: false),
                    tor_updated_at = table.Column<DateTime>(nullable: false),
                    tor_supplier_id = table.Column<string>(nullable: false),
                    tor_discount = table.Column<decimal>(type: "Money", nullable: false, defaultValue: 0m),
                    tor_total = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_order", x => x.tor_id);
                    table.ForeignKey(
                        name: "FK_tb_order_tb_supplier_tor_supplier_id",
                        column: x => x.tor_supplier_id,
                        principalTable: "tb_supplier",
                        principalColumn: "tsu_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_asset",
                columns: table => new
                {
                    tas_id = table.Column<string>(nullable: false),
                    tas_name = table.Column<string>(nullable: false),
                    tas_code = table.Column<string>(nullable: false),
                    tas_type_asset_id = table.Column<string>(nullable: false),
                    tas_price = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_asset", x => x.tas_id);
                    table.ForeignKey(
                        name: "FK_tb_asset_tb_type_asset_tas_type_asset_id",
                        column: x => x.tas_type_asset_id,
                        principalTable: "tb_type_asset",
                        principalColumn: "tta_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_item_order",
                columns: table => new
                {
                    tio_id = table.Column<string>(nullable: false),
                    tio_asset_id = table.Column<string>(nullable: false),
                    tio_quantity = table.Column<int>(nullable: false),
                    tio_unit_price = table.Column<decimal>(type: "Money", nullable: false),
                    tio_order_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_item_order", x => x.tio_id);
                    table.ForeignKey(
                        name: "FK_tb_item_order_tb_asset_tio_asset_id",
                        column: x => x.tio_asset_id,
                        principalTable: "tb_asset",
                        principalColumn: "tas_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_item_order_tb_order_tio_order_id",
                        column: x => x.tio_order_id,
                        principalTable: "tb_order",
                        principalColumn: "tor_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_asset_tas_type_asset_id",
                table: "tb_asset",
                column: "tas_type_asset_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_item_order_tio_asset_id",
                table: "tb_item_order",
                column: "tio_asset_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_item_order_tio_order_id",
                table: "tb_item_order",
                column: "tio_order_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_order_tor_supplier_id",
                table: "tb_order",
                column: "tor_supplier_id");
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
