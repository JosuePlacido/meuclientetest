using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
	[Table("tb_item_order")]
	public class ItemOrder : Entity
	{
		[Required]
		public string AssetId { get; set; }
		public Asset Asset { get; set; }
		[Required]
		public int Quantity { get; set; }
		[Required]
		[Column(TypeName = "Money")]
		public decimal UnitPrice { get; set; }
		[Required]
		public string OrderId { get; set; }
		public Order Order { get; set; }
	}
}
