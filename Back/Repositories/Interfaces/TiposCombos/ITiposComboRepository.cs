using Shared.DTOs;
using Shared.Entities;
using Shared.Responses;

namespace Back.Repositories.Interfaces.TiposCombos
{
	public interface ITiposComboRepository
	{
		Task<ActionResponse<IEnumerable<TiposCombo>>> GetFilter(FilterDTO filter);
	}
}
