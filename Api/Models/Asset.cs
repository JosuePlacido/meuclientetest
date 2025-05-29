using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
	[Table("tb_asset")]
	public class Asset : InternalScoped
	{
		[Required]
		public string TypeAssetId { get; protected set; }
		public TypeAsset TypeAsset { get; protected set; }
		[Required]
		[Column(TypeName = "Money")]
		public decimal Price { get; set; }

		public Asset(string name, string code, decimal price, string typeAssetId)
		{
			Name = name;
			Code = code;
			Price = price;
			TypeAssetId = typeAssetId;
			Price = price;
		}

		public void Update(string name, string code, decimal price, string typeAssetId)
		{
			if (!string.IsNullOrEmpty(name))
				Name = name;
			if (!string.IsNullOrEmpty(code))
				Code = code;
			if (price > 0)
				Price = price;
			if (!string.IsNullOrEmpty(typeAssetId))
				TypeAssetId = typeAssetId;
		}
	}
}
