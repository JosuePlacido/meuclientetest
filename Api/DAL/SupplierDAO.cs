using System.Threading.Tasks;
using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.DAL
{

	public interface IDAOSupplier : IDAO<Supplier>
	{
		Task<bool> ExistCNPJ(string cnpj);
	}
	public class DAOSupplier : DAOBase<Supplier>, IDAOSupplier
	{
		public DAOSupplier(Context context) : base(context)
		{
		}

		public async Task<bool> ExistCNPJ(string cnpj)
		{
			return await _context.Suppliers.AnyAsync(s => s.CNPJ.Value == cnpj);
		}
	}
}
