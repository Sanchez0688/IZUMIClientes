using Back.Data;
using Back.Helpers;
using Back.Repositories.Interfaces.TiposCombosDetalles;
using Shared.Entities;
using Shared.Responses;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Back.Repositories.Implementations.TiposCombosDetalles
{
	public class TiposComboDetalleRepository : ITiposComboDetalleRepository
	{
		private readonly DataContext _dfcontext;
		

		public TiposComboDetalleRepository(DataContext dfcontext)
		{
			_dfcontext = dfcontext;			
		}

		public async Task<ActionResponse<IEnumerable<TiposComboDetalle>>> Get(string IdCombo)
		{
			ActionResponse<IEnumerable<TiposComboDetalle>> actionResponse = new();
			try
			{
				Expression<Func<TiposComboDetalle, bool>> predicate = pre => true;
				predicate = SwapVisitor.CombineExpressions(predicate, pre => pre.Statu == true);
				predicate = SwapVisitor.CombineExpressions(predicate, pre => pre.Tipo.CodeName.Contains(IdCombo.Trim()));

				actionResponse.Result = await _dfcontext.TiposComboDetalles
									   .Include(det => det.Tipo)
									   .Where(predicate)
									   .ToListAsync();

				actionResponse.WasSuccess = true;
				actionResponse.Message = "Consulta ejecutada con éxito.";
				actionResponse.CodigoHTTP = 200;
				if (actionResponse.Result.Count() == 0)
				{
					actionResponse.Message = "No se encontraron resultados para el combo proporcionado.";
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
