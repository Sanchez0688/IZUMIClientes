using Back.Data;
using Back.Helpers;
using Back.Repositories.Interfaces.TiposCombos;
using Shared.DTOs;
using Shared.Entities;
using Shared.Responses;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Back.Repositories.Implementations.TiposCombos
{
    public class TiposComboRepository : ITiposComboRepository
    {
        private readonly DataContext _dfcontext;

        public TiposComboRepository(DataContext dfcontext)
        {
            _dfcontext = dfcontext;
        }

        public async Task<ActionResponse<IEnumerable<TiposCombo>>> GetFilter(FilterDTO filter)
        {
            ActionResponse<IEnumerable<TiposCombo>> actionResponse = new();
            Expression<Func<TiposCombo, bool>> predicate = pre => true;

            try
            {
				predicate = SwapVisitor.CombineExpressions(predicate, pre => pre.Statu==true);

				if (!string.IsNullOrEmpty(filter.Filtro))
                    predicate = SwapVisitor.CombineExpressions(predicate, pre => pre.Name.Contains(filter.Filtro.Trim()));

                actionResponse.Result = await _dfcontext.TiposCombos                                                     
                                                        .Where(predicate)
                                                        .ToListAsync();
                actionResponse.WasSuccess = true;
                
                actionResponse.CodigoHTTP = 200;
                if (actionResponse.Result.Count() == 0)
                {
					actionResponse.Message = "No se encontraron resultados para el filtro proporcionado.";
					actionResponse.CodigoHTTP = 404;
				}
                   
            }
            catch (Exception ex)
            {
                actionResponse.WasSuccess = false;
                actionResponse.Message = $"Ocurrió un error al ejecutar la consulta";
                actionResponse.CodigoHTTP = 500;
            }
            return actionResponse;
        }
    }
}
