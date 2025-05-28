using System.Threading.Tasks;
using Api.DTO;
using Api.Models;
using Api.Services.AssetCRUD;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[ApiController]
	[ApiVersion("1.0")]
	[ApiVersion("2.0")]
	[Route("api/v{version:apiVersion}/ativo")]
	public class AssetController : ControllerBase
	{
		private readonly IAssetService _service;
		public AssetController(IAssetService service)
		{
			_service = service;
		}
		[HttpGet, MapToApiVersion("1.0")]
		public async Task<ActionResult<Asset[]>> GetV1Async()
		{
			return await _service.GetAll();
		}
		[HttpGet, MapToApiVersion("2.0")]
		public async Task<ActionResult<PaginationDTO<Asset>>> GetV2Async(int page = 1, int take = 10)
		{
			return await _service.GetAllList(page, take);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Asset>> GetAsync(string id)
		{
			return await _service.Get(id);
		}

		[HttpPost]
		public async Task<ActionResult<Asset>> Post([FromBody] CreateAssetDTO values)
		{
			return await _service.Add(values);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<Asset>> Put(string id, [FromBody] UpdateAssetDTO values)
		{
			return await _service.Update(id, values);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<Asset>> Delete(string id)
		{
			return await _service.Remove(id);
		}
	}
}
