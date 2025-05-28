using System.Threading.Tasks;
using Api.Models;

namespace Api.Services.OrderCRUD
{
	public interface IOrderService
	{
		Task<ViewOrderItemListDTO[]> GetAll();
		Task<ViewOrderDetailedDTO> Get(string id);
		Task<ViewOrderDetailedDTO> Update(string id, UpdateOrderDTO newValues);
		Task<ViewOrderDetailedDTO> Register(CreateOrderDTO newValues);
		Task<ViewOrderDetailedDTO> Remove(string id);
	}
}
