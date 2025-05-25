using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
	[Table("tb_asset")]
	public class Asset : Item
	{
		[Required]
		public string TypeAssetId { get; set; }
		public TypeAsset TypeAsset { get; set; }
		[Required]
		[Column(TypeName = "Money")]
		public decimal Price { get; set; }
	}
}
