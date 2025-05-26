using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DAL;
using Api.Models;
using Api.Services.SupplierCRUD;
using Application.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
	[Route("api/fornecedor")]
	[ApiController]
	public class SupplierController : ControllerBase
	{
		private readonly ILogger<SupplierController> _logger;
		private readonly ISupplierService _service;
		public SupplierController(ISupplierService service, ILogger<SupplierController> logger)
		{
			_logger = logger;
			_service = service;
		}
		private async Task<ActionResult> TryProcess<T>(Task<T> applicationCall)
		{
			try
			{
				return Ok(await applicationCall);
			}
			catch (ValidationException ex)
			{
				foreach (var error in ex.Errors)
				{
					ModelState.AddModelError(error.Key, error.Message);
				}
				return BadRequest(ModelState);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Erro ao processar a requisição.");
				return StatusCode(500, "Ocorreu um erro inesperado.");
			}
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
			return await TryProcess(_service.Add(values));
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<ViewSupplierDTO>> Put(string id, [FromBody] UpdateSupplierDTO values)
		{
			return await TryProcess(_service.Update(id, values));
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<ViewSupplierDTO>> Delete(string id)
		{
			return await TryProcess(_service.Remove(id));
		}
	}
}
