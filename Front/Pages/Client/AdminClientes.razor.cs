using Blazored.Modal;
using Blazored.Modal.Services;
using Front.Repositories;
using Shared.Entities;
using Shared.Responses;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;


namespace Front.Pages.Client
{
    public partial class AdminClientes
    {
        private string? _searchString;
        private IEnumerable<Cliente>? _clientes;
        private Cliente _filtros = new();
        [CascadingParameter] private IModalService? Modal { get; set; }
    
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
      
        protected override void OnInitialized()
        {
            Task task = BuscarAsync();
        }


        private async Task BuscarAsync()
        {
            try
            {
                _clientes = [];
                 string url = "/api/Cliente/Get";
                

                var responseHttp = await Repository.GetAsync<ActionResponse<List<Cliente>>>(url);


                if (responseHttp.Error)
                {
                     var message = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                    return;
                }

                _clientes = responseHttp.Response!.Result;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                var logId = Guid.NewGuid().ToString();
                 await SweetAlertService.FireAsync("Error", $"{"Error al consultar los Clientes "} {"Codigo"} {logId}", SweetAlertIcon.Error);
                return;
            }
        }
		private async void UpdateClienteAsync(int Id)
		{
			IModalReference modalReference;
			modalReference = Modal!.Show<EditCliente>(string.Empty, new ModalParameters().Add("Id", Id));
			var resul = await modalReference.Result;
			if (resul.Confirmed)
			{
				await BuscarAsync();
			}
			StateHasChanged();
		}

		private async void EliminarClienteAsync(int id)
		{
			
				var result = await SweetAlertService.FireAsync(new SweetAlertOptions
				{
					Title = "Confirmación",
					Text = "¿Esta seguro que quieres borrar el registro?",
					Icon = SweetAlertIcon.Question,
					ShowCancelButton = true
				});
				var confirm = string.IsNullOrEmpty(result.Value);

				if (confirm)
				{
					return;
				}

				var responseHttp = await Repository.DeleteAsync<Cliente>($"api/Cliente/Delete/{id}");
				if (responseHttp.Error)
				{
					if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
					{
						
						return;
					}

					var mensajeError = await responseHttp.GetErrorMessageAsync();
					await SweetAlertService.FireAsync("Error", mensajeError, SweetAlertIcon.Error);
					return;
				}

			await BuscarAsync();

		}

        private async Task NewClienteAsync()
        {
            if (Modal == null)
            {
                Console.WriteLine("Modal service is not initialized.");
                return;
            }

            IModalReference modalReference;
            modalReference = Modal.Show<CreateCliente>();
            var resul = await modalReference.Result;
            if (resul.Confirmed)
            {
                await BuscarAsync();
            }
            StateHasChanged();
        }

        private Func<Cliente, bool> QuickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.PrimerNombre.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.PrimerApellido.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (x.NumeroDocumento.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };
       

    }
}