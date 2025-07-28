using Back.Facade.Interfaces.TiposCombos;
using Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers.TiposCombos
{
    [ApiController]    
    [Route("api/[controller]")]
    public class TiposComboController : ControllerBase
    {
        private readonly ITiposComboFacade _UnitOfWork;

        public TiposComboController(ITiposComboFacade UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }

        [HttpGet("GetFilter")]
        public async Task<IActionResult> GetFilter([FromQuery] FilterDTO filter)
        {
			var result = await _UnitOfWork.GetFilter(filter);
			
			return StatusCode(result.CodigoHTTP, result);
		}
    }
}
