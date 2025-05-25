using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
	[Table("tb_supplier")]
	public class Supplier : Item
	{
		[Required]
		public CNPJ CNPJ { get; set; }

		protected Supplier() { }

	}
}
