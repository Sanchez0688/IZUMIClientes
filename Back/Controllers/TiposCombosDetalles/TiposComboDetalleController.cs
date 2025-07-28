using Back.Facade.Interfaces.TiposCombosDetalles;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers.TiposCombosDetalles
{
	[ApiController]	
	[Route("api/[controller]")]
	public class TiposComboDetalleController : ControllerBase
	{
		private readonly ITiposComboDetalleFacade _UnitOfWork;

		public TiposComboDetalleController(ITiposComboDetalleFacade UnitOfWork)
		{
			_UnitOfWork = UnitOfWork;
		}

		[HttpGet("Get/{Combo}")]
		public async Task<IActionResult> Get(string Combo)
		{
			var result = await _UnitOfWork.Get(Combo);

			return StatusCode(result.CodigoHTTP, result);
		}
	}
}
