using System.Threading.Tasks;
using Api.DTO;
using Api.Models;

namespace Api.Services.AssetCRUD
{
	public interface IAssetService
	{
		Task<Asset[]> GetAll();
		Task<PaginationDTO<Asset>> GetAllList(int page, int take);
		Task<Asset> Get(string id);
		Task<Asset> Update(string id, UpdateAssetDTO newValues);
		Task<Asset> Add(CreateAssetDTO newValues);
		Task<Asset> Remove(string id);
	}
}
