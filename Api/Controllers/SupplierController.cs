using System.Threading.Tasks;
using Api.Services.SupplierCRUD;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[Route("api/fornecedor")]
	[ApiController]
	public class SupplierController : ControllerBase
	{
		private readonly ISupplierService _service;
		public SupplierController(ISupplierService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<ActionResult<ViewSupplierDTO[]>> GetAsync()
		{
			return await _service.GetAll();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ViewSupplierDTO>> GetAsync(string id)
		{
			return await _service.Get(id);
		}

		[HttpPost]
		public async Task<ActionResult<ViewSupplierDTO>> Post([FromBody] CreateSupplierDTO values)
		{
			return await _service.Add(values);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<ViewSupplierDTO>> Put(string id, [FromBody] UpdateSupplierDTO values)
		{
			return await _service.Update(id, values);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<ViewSupplierDTO>> Delete(string id)
		{
			return await _service.Remove(id);
		}
	}
}
