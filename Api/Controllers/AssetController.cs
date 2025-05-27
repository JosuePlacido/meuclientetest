using System.Threading.Tasks;
using Api.Models;
using Api.Services.AssetCRUD;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[Route("api/ativo")]
	[ApiController]
	public class AssetController : ControllerBase
	{
		private readonly IAssetService _service;
		public AssetController(IAssetService service)
		{
			_service = service;
		}
		[HttpGet]
		public async Task<ActionResult<Asset[]>> GetAsync()
		{
			return await _service.GetAll();
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
