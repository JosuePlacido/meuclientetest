using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.Services.AssetCRUD
{
	public class UpdateAssetDTO
	{
		public string Name { get; set; }
		public string Code { get; set; }
		[Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
		public decimal Price { get; set; }

		public string TypeAssetId { get; set; }
		public TypeAsset TypeAsset { get; set; }
	}
	public class CreateAssetDTO
	{
		[Required(ErrorMessage = "O campo {0} é obrigatório.")]
		public string Name { get; set; }
		[Required(ErrorMessage = "O campo {0} é obrigatório.")]
		public string Code { get; set; }
		[Required(ErrorMessage = "O campo {0} é obrigatório.")]
		[Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
		[DataType(DataType.Currency)]
		public decimal Price { get; set; }

		public string TypeAssetId { get; set; }
		public TypeAsset TypeAsset { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (string.IsNullOrWhiteSpace(TypeAssetId) && TypeAsset == null)
			{
				yield return new ValidationResult(
					"Tipo de ativo é um campo obrigatório",
					new[] { nameof(TypeAssetId), nameof(TypeAsset) }
				);
			}
		}

		public Asset ToAsset()
		{
			return new Asset(Name, Code, Price, TypeAssetId); ;
		}
	}
}
