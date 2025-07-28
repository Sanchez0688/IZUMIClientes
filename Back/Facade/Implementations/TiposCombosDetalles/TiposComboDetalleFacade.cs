using Back.Repositories.Interfaces.TiposCombosDetalles;
using Back.Facade.Interfaces.TiposCombosDetalles;
using Shared.Entities;
using Shared.Responses;

namespace Back.Facade.Implementations.TiposCombosDetalles
{
	public class TiposComboDetalleFacade : ITiposComboDetalleFacade
	{
		private readonly ITiposComboDetalleRepository _Repository;

		public TiposComboDetalleFacade(ITiposComboDetalleRepository Repository)
		{
			_Repository = Repository;
		}
		public async Task<ActionResponse<IEnumerable<TiposComboDetalle>>> Get(string IdCombo) => await _Repository.Get( IdCombo);
		

	}
}
