using System.Threading.Tasks;
using Api.DAL;
using Api.Models;
using Application.Exceptions;

namespace Api.Services.AssetCRUD
{
	public class AssetService : IAssetService
	{
		private readonly IDAOAsset _daoAsset;
		private readonly IDAOTypeAsset _daoTypeAsset;

		public AssetService(IDAOAsset daoAsset, IDAOTypeAsset daoTypeAsset)
		{
			_daoAsset = daoAsset;
			_daoTypeAsset = daoTypeAsset;
		}

		public async Task<Asset> Add(CreateAssetDTO newValues)
		{
			Asset asset;
			if (string.IsNullOrEmpty(newValues.TypeAssetId))
			{
				TypeAsset typeAssetCreated = await _daoTypeAsset.Add(newValues.TypeAsset);
				newValues.TypeAssetId = typeAssetCreated.Id;
			}

			asset = await _daoAsset.Add(newValues.ToAsset());
			await _daoAsset.Commit();
			return asset;
		}

		public async Task<Asset> Get(string id)
		{
			Asset asset = await _daoAsset.GetAssetDetailed(id);
			return asset;
		}

		public async Task<Asset[]> GetAll()
		{
			return await _daoAsset.GetAll();
		}

		public async Task<Asset> Remove(string id)
		{
			if (!await _daoAsset.Exists(id))
			{
				throw new ValidationException("Ativo não encontrado",
					new ValidationError("id", "nenhum Ativo encontrado com este id"));
			}
			Asset asset = await _daoAsset.GetById(id);
			_daoAsset.Delete(asset);
			await _daoAsset.Commit();

			return asset;
		}

		public async Task<Asset> Update(string id, UpdateAssetDTO newValues)
		{
			Asset asset = await _daoAsset.GetById(id);
			if (asset == null)
			{
				throw new ValidationException("Ativo não encontrado",
					new ValidationError("id", "nenhum Ativo encontrado com este id"));
			}
			if (string.IsNullOrEmpty(newValues.TypeAssetId) && newValues.TypeAsset != null)
			{
				TypeAsset typeAssetCreated = await _daoTypeAsset.Add(newValues.TypeAsset);
				newValues.TypeAssetId = typeAssetCreated.Id;
			}

			asset.Update(newValues.Name, newValues.Code, newValues.Price, newValues.TypeAssetId);
			asset = _daoAsset.Update(asset);
			await _daoAsset.Commit();

			return asset;
		}
	}
}
