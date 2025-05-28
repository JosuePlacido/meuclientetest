using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Data.Migrations
{
	public partial class CreateTriggerTableOrderUpdateAt : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql(
	@"
CREATE OR REPLACE FUNCTION func_update_at_tb_order()
RETURNS TRIGGER AS $$
BEGIN
    -- Atualiza a coluna que você quer
    NEW.tor_updated_at = now();
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;
    ");

			migrationBuilder.Sql(
				@"
			CREATE TRIGGER set_pedido_updated_at
			BEFORE UPDATE ON tb_order
			FOR EACH ROW
			EXECUTE FUNCTION func_update_at_tb_order('tor_updated_at');
    ");


		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{

		}
	}
}
