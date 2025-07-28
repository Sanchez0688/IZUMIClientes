using Back.Data;
using Back.Repositories.Interfaces.Clientes;
using Shared.DTOs;
using Shared.Entities;
using Shared.Responses;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Back.Repositories.Implementations.Clientes
{
	public class ClientesRepository : IClientesRepository
	{
		private readonly DataContext _dfcontext;
		public ClientesRepository(DataContext dfcontext)
		{
			_dfcontext = dfcontext;
		}

		public async Task<ActionResponse<string>> Create(ClienteDTO clienteDTO)
		{
			var actionResponse = new ActionResponse<string>();

			try
			{
				// Verificar si ya existe un cliente con el mismo TipoDocumento y NumeroDocumento
				var clienteExistente = await _dfcontext.Clientes
					.AnyAsync(x => x.IdTipoDocumento == clienteDTO.IdTipoDocumento
								&& x.NumeroDocumento.Trim() == clienteDTO.NumeroDocumento.Trim());

				if (clienteExistente)
				{					
					actionResponse.Result = "Cliente ya se encuentra registrado.";
					actionResponse.WasSuccess = false;
					actionResponse.Message = "Cliente ya se encuentra registrado.";
					actionResponse.CodigoHTTP = 409; // Conflict
				}
				else
				{
					// Convertir ClienteDTO a Cliente
					var cliente = new Cliente
					{
						IdTipoDocumento = clienteDTO.IdTipoDocumento,
						NumeroDocumento = clienteDTO.NumeroDocumento,
						FechaNacimiento = clienteDTO.FechaNacimiento ?? DateTime.Now,
						PrimerNombre = clienteDTO.PrimerNombre,
						SegundoNombre = clienteDTO.SegundoNombre,
						PrimerApellido = clienteDTO.PrimerApellido,
						SegundoApellido = clienteDTO.SegundoApellido,
						DireccionResidencia = clienteDTO.DireccionResidencia,
						NumeroCelular = clienteDTO.NumeroCelular,
						Email = clienteDTO.Email,
						IdPlanPreferencia = clienteDTO.IdPlanPreferencia
					};

					_dfcontext.Clientes.Add(cliente);
					await _dfcontext.SaveChangesAsync();

					actionResponse.Result = "Cliente creado con éxito.";
					actionResponse.WasSuccess = true;
					actionResponse.Message = "Cliente creado con éxito.";
					actionResponse.CodigoHTTP = 201; // Created
				}
			}
			catch (Exception ex)
			{
				actionResponse.WasSuccess = false;
				actionResponse.Message = $"Error al crear el cliente: {ex.Message}";
				actionResponse.CodigoHTTP = 500; // Internal Server Error
			}

			return actionResponse;
		}


		public async Task<ActionResponse<string>> Delete(int id)
		{
			var actionResponse = new ActionResponse<string>();

			try
			{
				var cliente = await _dfcontext.Clientes.FindAsync(id);

				if (cliente == null)
				{
					actionResponse.WasSuccess = false;
					actionResponse.Message = "Cliente no encontrado.";
					actionResponse.CodigoHTTP = 404; // Not Found
					return actionResponse;
				}

				_dfcontext.Clientes.Remove(cliente);
				await _dfcontext.SaveChangesAsync();

				actionResponse.WasSuccess = true;
				actionResponse.Message = "Cliente eliminado con éxito.";
				actionResponse.CodigoHTTP = 200; // OK
			}
			catch (Exception ex)
			{
				actionResponse.WasSuccess = false;
				actionResponse.Message = $"Error al eliminar el cliente: {ex.Message}";
				actionResponse.CodigoHTTP = 500; // Internal Server Error
			}

			return actionResponse;
		}

		public async Task<ActionResponse<IEnumerable<Cliente>>> Get()
		{
			ActionResponse<IEnumerable<Cliente>> actionResponse = new();
			Expression<Func<TiposCombo, bool>> predicate = pre => true;

			try
			{			

				actionResponse.Result = await _dfcontext.Clientes
														.Include(x => x.PlanPreferencia)
														.Include(x => x.TipoDocumento)
														.ToListAsync();
				actionResponse.WasSuccess = true;

				actionResponse.CodigoHTTP = 200;
				if (actionResponse.Result.Count() == 0)
				{
					actionResponse.Message = "No se encontraron Clientes.";
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

		public async Task<ActionResponse<Cliente>> Get(int id)
		{
			ActionResponse<Cliente> actionResponse = new();
			Expression<Func<TiposCombo, bool>> predicate = pre => true;

			try
			{

				actionResponse.Result = await _dfcontext.Clientes
														.Include(x => x.PlanPreferencia)
														.Include(x => x.TipoDocumento)
														.Where(x => x.IdCliente== id)
														.FirstOrDefaultAsync();
				actionResponse.WasSuccess = true;

				actionResponse.CodigoHTTP = 200;
				if (actionResponse.Result ==null)
				{
					actionResponse.Message = "No se encontro el Cliente.";
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

		public async Task<ActionResponse<string>> Update(ClienteDTO clienteDTO)
		{
			var actionResponse = new ActionResponse<string>();

			try
			{
				var existingCliente = await _dfcontext.Clientes.FindAsync(clienteDTO.IdCliente);

				if (existingCliente == null)
				{
					actionResponse.WasSuccess = false;
					actionResponse.Message = "Cliente no encontrado.";
					actionResponse.CodigoHTTP = 404; // Not Found
					return actionResponse;
				}

                // Verificar si ya existe un cliente con el mismo TipoDocumento y NumeroDocumento
                var clienteExistente = await _dfcontext.Clientes
                    .AnyAsync(x => x.IdTipoDocumento == clienteDTO.IdTipoDocumento
                                && x.NumeroDocumento.Trim() == clienteDTO.NumeroDocumento.Trim());

				if (clienteExistente)
				{
					actionResponse.Result = "Cliente ya se encuentra registrado.";
					actionResponse.WasSuccess = false;
					actionResponse.Message = "Cliente ya se encuentra registrado.";
					actionResponse.CodigoHTTP = 409; // Conflict
				}
				else
				{
					existingCliente.IdTipoDocumento = clienteDTO.IdTipoDocumento;
					existingCliente.NumeroDocumento = clienteDTO.NumeroDocumento;
					existingCliente.FechaNacimiento = clienteDTO.FechaNacimiento ?? DateTime.Now;
					existingCliente.PrimerNombre = clienteDTO.PrimerNombre;
					existingCliente.SegundoNombre = clienteDTO.SegundoNombre;
					existingCliente.PrimerApellido = clienteDTO.PrimerApellido;
					existingCliente.SegundoApellido = clienteDTO.SegundoApellido;
					existingCliente.DireccionResidencia = clienteDTO.DireccionResidencia;
					existingCliente.NumeroCelular = clienteDTO.NumeroCelular;
					existingCliente.Email = clienteDTO.Email;
					existingCliente.IdPlanPreferencia = clienteDTO.IdPlanPreferencia;

					await _dfcontext.SaveChangesAsync();

					actionResponse.WasSuccess = true;
					actionResponse.Message = "Cliente actualizado con éxito.";
					actionResponse.CodigoHTTP = 200; // OK
				}
			}
			catch (Exception ex)
			{
				actionResponse.WasSuccess = false;
				actionResponse.Message = $"Error al actualizar el cliente: {ex.Message}";
				actionResponse.CodigoHTTP = 500; // Internal Server Error
			}

			return actionResponse;
		}
	}
}
