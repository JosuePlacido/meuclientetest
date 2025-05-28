using System;
using System.Threading.Tasks;
using Api.Models;
using Api.Services.OrderCRUD;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[Route("api/contrato-venda")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _service;
		public OrderController(IOrderService service)
		{
			_service = service;
		}
		[HttpGet]
		public async Task<ActionResult<ViewOrderItemListDTO[]>> GetAsync()
		{
			return await _service.GetAll();
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
