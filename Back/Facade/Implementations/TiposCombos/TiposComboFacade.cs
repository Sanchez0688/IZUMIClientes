using Back.Repositories.Interfaces.TiposCombos;
using Back.Facade.Interfaces.TiposCombos;
using Shared.DTOs;
using Shared.Entities;
using Shared.Responses;

namespace Back.Facade.Implementations.TiposCombos
{
    public class TiposComboFacade : ITiposComboFacade
	{
		private readonly ITiposComboRepository _Repository;

		public TiposComboFacade(ITiposComboRepository Repository)
		{
			_Repository = Repository;
		}

		public async Task<ActionResponse<IEnumerable<TiposCombo>>> GetFilter(FilterDTO filter) => await _Repository.GetFilter(filter);
	}
}
