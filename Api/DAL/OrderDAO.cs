using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.DTO;
using Api.Models;
using Api.Services.OrderCRUD;
using Microsoft.EntityFrameworkCore;

namespace Api.DAL
{

	public interface IDAOOrder : IDAO<Order>
	{
		Task<Order> GetDetailedById(string id);
	}
	public class DAOOrder : DAOBase<Order>, IDAOOrder
	{
		public DAOOrder(Context context) : base(context)
		{
		}


		public async Task<Order> GetDetailedById(string id)
		{
			return await _context.Orders
				.Include(o => o.Supplier)
				.Include(o => o.Items)
					.ThenInclude(i => i.Asset)
						.ThenInclude(a => a.TypeAsset)
				.FirstOrDefaultAsync(o => o.Id == id);
		}
		public override async Task<Order[]> GetAll()
		{
			return await _context.Orders
				.Include(o => o.Supplier)
				.ToArrayAsync();
		}
		public override async Task<PaginationDTO<Order>> List(int page, int take)
		{
			var query = _context.Orders.AsNoTracking();
			PaginationDTO<Order> result = new PaginationDTO<Order>()
			{
				Items = await query
					.Include(o => o.Supplier).Skip((page - 1) * take).Take(take).ToArrayAsync(),
				Total = await query.CountAsync(),
				Page = page,
				Take = take
			};
			return result;
		}
		public override Order Update(Order obj)
		{
			ItemOrder[] itemsPersisted = _context.ItemOrders
				.Where(io => io.OrderId == obj.Id).ToArray();
			_context.ItemOrders.RemoveRange(itemsPersisted);
			return base.Update(obj);
		}
	}
}
