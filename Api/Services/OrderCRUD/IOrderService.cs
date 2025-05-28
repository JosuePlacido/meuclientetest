using System.Threading.Tasks;
using Api.DTO;

namespace Api.Services.OrderCRUD
{
	public interface IOrderService
	{
		Task<ViewOrderItemListDTO[]> GetAll();
		Task<PaginationDTO<ViewOrderDetailedDTO>> GetAllList(int page, int take);
		Task<ViewOrderDetailedDTO> Get(string id);
		Task<ViewOrderDetailedDTO> Update(string id, UpdateOrderDTO newValues);
		Task<ViewOrderDetailedDTO> Register(CreateOrderDTO newValues);
		Task<ViewOrderDetailedDTO> Remove(string id);
	}
}
