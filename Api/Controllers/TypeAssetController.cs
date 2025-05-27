
using System.Threading.Tasks;
using Api.Models;
using Api.Services.TypeAssetCRUD;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[Route("api/tipoativo")]
	[ApiController]
	public class TypeAssetController : ControllerBase
	{
		private readonly ITypeAssetService _service;
		public TypeAssetController(ITypeAssetService service)
		{
			_service = service;
		}
		[HttpGet]
		public async Task<ActionResult<TypeAsset[]>> GetAsync()
		{
			return await _service.GetAll();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<TypeAsset>> GetAsync(string id)
		{
			return await _service.Get(id);
		}

		[HttpPost]
		public async Task<ActionResult<TypeAsset>> Post([FromBody] CreateTypeAssetDTO values)
		{
			return await _service.Add(values.ToTypeAsset());
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<TypeAsset>> Put(string id, [FromBody] UpdateTypeAssetDTO values)
		{
			return await _service.Update(id, values);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<TypeAsset>> Delete(string id)
		{
			return await _service.Remove(id);
		}
	}
}
