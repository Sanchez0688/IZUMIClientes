using Shared.DTOs;
using Shared.Entities;
using Shared.Responses;

namespace Back.Repositories.Interfaces.Clientes
{
	public interface IClientesRepository
	{
		Task<ActionResponse<IEnumerable<Cliente>>> Get();
		Task<ActionResponse<Cliente>> Get(int id);
		Task<ActionResponse<string>> Create(ClienteDTO cliente);
		Task<ActionResponse<string>> Update(ClienteDTO cliente);
		Task<ActionResponse<string>> Delete(int id);
	}
}
