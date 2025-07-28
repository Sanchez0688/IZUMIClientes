using Back.Facade.Interfaces.Clientes;
using Shared.DTOs;
using Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers.Clientes
{
	[ApiController]
	[Route("api/[controller]")]
	public class ClienteController : ControllerBase
	{
		private readonly IClienteFacade _UnitOfWork;

		public ClienteController(IClienteFacade UnitOfWork)
		{
			_UnitOfWork = UnitOfWork;
		}

		[HttpGet("Get")]
		public async Task<IActionResult> GetFilter()
		{
			var result = await _UnitOfWork.Get();

			return StatusCode(result.CodigoHTTP, result);
		}

		[HttpGet("Get/{Id}")]
		public async Task<IActionResult> Get(int Id)
		{
			var result = await _UnitOfWork.Get(Id);

			return StatusCode(result.CodigoHTTP, result);
		}
		[HttpPost("Create")]
		public async Task<IActionResult> Create([FromBody] ClienteDTO cliente)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Los datos proporcionados son inválidos.");
			}

			var result = await _UnitOfWork.Create(cliente);

			return StatusCode(result.CodigoHTTP, result);
		}

		[HttpPut("Update/{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] ClienteDTO cliente)
		{
			if (id != cliente.IdCliente)
			{
				return BadRequest("El ID del cliente no coincide.");
			}

			if (!ModelState.IsValid)
			{
				return BadRequest("Los datos proporcionados son inválidos.");
			}

			var result = await _UnitOfWork.Update(cliente);

			return StatusCode(result.CodigoHTTP, result);
		}

		[HttpDelete("Delete/{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await _UnitOfWork.Delete(id);

			return StatusCode(result.CodigoHTTP, result);
		}


	}
}
