using System.Threading.Tasks;
using Api.DAL;
using Api.Models;
using Api.Services.SupplierCRUD;
using Application.DTO;

namespace Api.Services.TypeAssetCRUD
{
	public class TypeAssetService : ITypeAssetService
	{
		private readonly IDAOTypeAsset _daoTypeAsset;

		public TypeAssetService(IDAOTypeAsset daoTypeAsset)
		{
			_daoTypeAsset = daoTypeAsset;
		}

		public async Task<TypeAsset> Add(TypeAsset newValues)
		{
			TypeAsset typeAsset = await _daoTypeAsset.Add(newValues);
			await _daoTypeAsset.Commit();
			return typeAsset;
		}

		public async Task<TypeAsset> Get(string id)
		{
			TypeAsset typeAsset = await _daoTypeAsset.GetById(id);
			return typeAsset;
		}

		public async Task<TypeAsset[]> GetAll()
		{
			return await _daoTypeAsset.GetAll();
		}

		public async Task<TypeAsset> Remove(string id)
		{
			TypeAsset typeAsset = await _daoTypeAsset.GetById(id);
			if (typeAsset == null)
			{
				throw new ValidationException("fornecedor não encontrado",
					new ValidationError("id", "nenhum fornecedor encontrado com este id"));
			}
			_daoTypeAsset.Delete(typeAsset);
			await _daoTypeAsset.Commit();

			return typeAsset;
		}

		public async Task<TypeAsset> Update(string id, UpdateTypeAssetDTO newValues)
		{
			TypeAsset typeAsset = await _daoTypeAsset.GetById(id);
			if (typeAsset == null)
			{
				throw new ValidationException("fornecedor não encontrado",
					new ValidationError("id", "nenhum fornecedor encontrado com este id"));
			}
			typeAsset.Update(newValues.Name, newValues.Code);
			typeAsset = _daoTypeAsset.Update(typeAsset);
			await _daoTypeAsset.Commit();

			return typeAsset;
		}
	}
}
