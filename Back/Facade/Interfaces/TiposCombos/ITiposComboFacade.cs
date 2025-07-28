using Shared.DTOs;
using Shared.Entities;
using Shared.Responses;

namespace Back.Facade.Interfaces.TiposCombos
{
    public interface ITiposComboFacade
    {
        Task<ActionResponse<IEnumerable<TiposCombo>>> GetFilter(FilterDTO filter);
    }
}
