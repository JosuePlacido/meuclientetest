using System.Threading.Tasks;
using Api.Models;

namespace Api.Services.SupplierCRUD
{
	public interface ISupplierService
	{
		Task<ViewSupplierDTO[]> GetAll();
		Task<ViewSupplierDTO> Get(string id);
		Task<ViewSupplierDTO> Update(string id, UpdateSupplierDTO newValues);
		Task<ViewSupplierDTO> Add(CreateSupplierDTO newValues);
		Task<ViewSupplierDTO> Remove(string id);
	}
}
