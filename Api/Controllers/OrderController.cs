using System;
using System.Threading.Tasks;
using Api.DTO;
using Api.Models;
using Api.Services.OrderCRUD;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[ApiController]
	[ApiVersion("1.0")]
	[ApiVersion("2.0")]
	[Route("api/v{version:apiVersion}/contrato-venda")]
	[ApiExplorerSettings(GroupName = "Ativo")]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _service;
		public OrderController(IOrderService service)
		{
			_service = service;
		}
		[HttpGet, MapToApiVersion("1.0")]
		public async Task<ActionResult<ViewOrderItemListDTO[]>> GetV1Async()
		{
			return await _service.GetAll();
		}
		[HttpGet, MapToApiVersion("2.0")]
		public async Task<ActionResult<PaginationDTO<ViewOrderDetailedDTO>>> GetV2Async(int page = 1, int take = 10)
		{
			return await _service.GetAllList(page, take);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ViewOrderDetailedDTO>> GetAsync(string id)
		{
			return await _service.Get(id);
		}

		[HttpPost]
		public async Task<ActionResult<ViewOrderDetailedDTO>> Post([FromBody] CreateOrderDTO values)
		{
			return await _service.Register(values);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<ViewOrderDetailedDTO>> Put(string id, [FromBody] UpdateOrderDTO values)
		{
			return await _service.Update(id, values);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<ViewOrderDetailedDTO>> Delete(string id)
		{
			return await _service.Remove(id);
		}
	}
}
