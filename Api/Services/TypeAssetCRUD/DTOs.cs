using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.Services.SupplierCRUD
{
	public class UpdateTypeAssetDTO
	{
		public string Name { get; set; }
		public string Code { get; set; }
	}
}
