using Shared.Entities;
using Shared.Responses;

namespace Back.Repositories.Interfaces.TiposCombosDetalles
{
	public interface ITiposComboDetalleRepository
	{
		Task<ActionResponse<IEnumerable<TiposComboDetalle>>> Get(string IdCombo);
	}
}
