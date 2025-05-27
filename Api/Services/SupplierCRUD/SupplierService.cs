using System.Threading.Tasks;
using System.Linq;
using Api.DAL;
using Api.Models;
using Application.DTO;

namespace Api.Services.SupplierCRUD
{
	public class SupplierService : ISupplierService
	{
		private readonly IDAOSupplier _daoSupplier;

		public SupplierService(IDAOSupplier daoSupplier)
		{
			_daoSupplier = daoSupplier;
		}

		public async Task<ViewSupplierDTO> Add(CreateSupplierDTO newValues)
		{
			Supplier supplier = newValues.ToSuplier();
			if (await _daoSupplier.ExistCNPJ(supplier.CNPJ.Value))
			{
				throw new ValidationException("CNPJ usado",
					new ValidationError("cnpj", "CNPJ já está em uso"));
			}
			supplier = await _daoSupplier.Add(supplier);
			await _daoSupplier.Commit();
			return new ViewSupplierDTO(supplier);
		}

		public async Task<ViewSupplierDTO> Get(string id)
		{
			Supplier supplier = await _daoSupplier.GetById(id);
			if (supplier == null) return null;
			return new ViewSupplierDTO(supplier);
		}

		public async Task<ViewSupplierDTO[]> GetAll()
		{
			var suppliers = await _daoSupplier.GetAll();
			return suppliers.Select(s => new ViewSupplierDTO(s)).ToArray();
		}

		public async Task<ViewSupplierDTO> Remove(string id)
		{
			Supplier supplier = await _daoSupplier.GetById(id);
			if (supplier == null)
			{
				throw new ValidationException("Fornecedor não encontrado",
					new ValidationError("id", "nenhum Fornecedor encontrado com este id"));
			}
			_daoSupplier.Delete(supplier);
			await _daoSupplier.Commit();

			return new ViewSupplierDTO(supplier);
		}

		public async Task<ViewSupplierDTO> Update(string id, UpdateSupplierDTO newValues)
		{
			Supplier supplier = await _daoSupplier.GetById(id);
			if (supplier == null)
			{
				throw new ValidationException("Fornecedor não encontrado",
					new ValidationError("id", "nenhum Fornecedor encontrado com este id"));
			}
			supplier.Update(newValues.Name, newValues.Code);
			supplier = _daoSupplier.Update(supplier);
			await _daoSupplier.Commit();

			return new ViewSupplierDTO(supplier);
		}
	}
}
