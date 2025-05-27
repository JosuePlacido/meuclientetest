using System.Threading.Tasks;
using Api.Models;
using Api.Services.SupplierCRUD;

namespace Api.Services.TypeAssetCRUD
{
	public interface ITypeAssetService
	{
		Task<TypeAsset[]> GetAll();
		Task<TypeAsset> Get(string id);
		Task<TypeAsset> Update(string id, UpdateTypeAssetDTO newValues);
		Task<TypeAsset> Add(TypeAsset newValues);
		Task<TypeAsset> Remove(string id);
	}
}
