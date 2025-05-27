using System.Threading.Tasks;
using Api.Models;
using Api.Services.SupplierCRUD;

namespace Api.Services.AssetCRUD
{
	public interface IAssetService
	{
		Task<Asset[]> GetAll();
		Task<Asset> Get(string id);
		Task<Asset> Update(string id, UpdateAssetDTO newValues);
		Task<Asset> Add(CreateAssetDTO newValues);
		Task<Asset> Remove(string id);
	}
}
