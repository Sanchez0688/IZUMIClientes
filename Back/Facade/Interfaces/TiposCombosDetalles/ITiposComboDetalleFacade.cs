using Shared.Entities;
using Shared.Responses;

namespace Back.Facade.Interfaces.TiposCombosDetalles
{
	public interface ITiposComboDetalleFacade
	{
		Task<ActionResponse<IEnumerable<TiposComboDetalle>>> Get(string IdCombo);
	}
}
