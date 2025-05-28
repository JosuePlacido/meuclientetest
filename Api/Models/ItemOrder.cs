using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Newtonsoft.Json;

namespace Api.Models
{
	[Table("tb_item_order")]
	public class ItemOrder : Entity
	{
		[Required]
		public string AssetId { get; private set; }
		public Asset Asset { get; private set; }
		[Required]
		public int Quantity { get; private set; }
		[Required]
		[Column(TypeName = "Money")]
		public decimal UnitPrice { get; private set; }
		[Required]
		public string OrderId { get; private set; }

		public ItemOrder(string assetId, int quantity, decimal unitPrice)
		{
			AssetId = assetId;
			Quantity = quantity;
			UnitPrice = unitPrice;
		}
	}
}
