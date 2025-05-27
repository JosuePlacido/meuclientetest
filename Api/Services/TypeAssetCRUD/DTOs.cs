using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.Services.TypeAssetCRUD
{
	public class UpdateTypeAssetDTO
	{
		public string Name { get; set; }
		public string Code { get; set; }
	}
	public class CreateTypeAssetDTO
	{
		[Required(ErrorMessage = "O campo {0} é obrigatório.")]
		public string Name { get; protected set; }
		[Required(ErrorMessage = "O campo {0} é obrigatório.")]
		public string Code { get; protected set; }

		public TypeAsset ToTypeAsset()
		{
			return new TypeAsset(Name, Code);
		}
	}
}
