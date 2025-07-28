using Back.Repositories.Interfaces.Clientes;
using Back.Facade.Interfaces.Clientes;
using Shared.DTOs;
using Shared.Entities;
using Shared.Responses;

namespace Back.Facade.Implementations.Clientes
{
	public class ClienteFacede : IClienteFacade
	{
		private readonly IClientesRepository _Repository;

		public ClienteFacede(IClientesRepository Repository)
		{
			_Repository = Repository;
		}

		public async Task<ActionResponse<string>> Create(ClienteDTO cliente) => await _Repository.Create(cliente);

		public async Task<ActionResponse<string>> Delete(int id) => await _Repository.Delete(id);

		public async Task<ActionResponse<IEnumerable<Cliente>>> Get() => await _Repository.Get();
		public async Task<ActionResponse<Cliente>> Get(int id) => await _Repository.Get(id);

		public async Task<ActionResponse<string>> Update(ClienteDTO cliente) => await _Repository.Update(cliente);
	}
}
