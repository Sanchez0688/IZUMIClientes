using Blazored.Modal;
using Blazored.Modal.Services;
using Front.Repositories;
using Shared.DTOs;
using Shared.Entities;
using Shared.Responses;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.ComponentModel.DataAnnotations;

namespace Front.Pages.Client
{
    public partial class EditCliente
    {
		private Cliente _cliente = new();
		private ClienteDTO _ClienteDTO = new();
		private bool _statusId;
		[Inject] private IRepository Repository { get; set; } = null!;
		[Inject] private ISnackbar Snackbar { get; set; } = null!;
		[Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

		[CascadingParameter] BlazoredModalInstance ModalInstance { get; set; } = default!;
		[EditorRequired, Parameter] public int Id { get; set; }
		private List<TiposComboDetalle>? tipoDocumento;
		private List<TiposComboDetalle>? planPref;
		
		private async Task LoadTipoDocAsync()
		{
			string url = "/api/TiposComboDetalle/Get/TIPODOC";
			var responseHttp = await Repository.GetAsync<ActionResponse<List<TiposComboDetalle>>>(url);
			if (responseHttp.Error)
			{
				var message = await responseHttp.GetErrorMessageAsync();
				await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
				return;
			}

			tipoDocumento = responseHttp.Response!.Result;
		}
		private async Task LoadplanAsync()
		{
			string url = "/api/TiposComboDetalle/Get/PLANPREF";
			var responseHttp = await Repository.GetAsync<ActionResponse<List<TiposComboDetalle>>>(url);
			if (responseHttp.Error)
			{
				var message = await responseHttp.GetErrorMessageAsync();
				await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
				return;
			}

			planPref = responseHttp.Response!.Result;
		}
		protected override async Task OnParametersSetAsync()
		{
			
			try
			{
				await LoadTipoDocAsync();
				await LoadplanAsync();

				if (Id == 0)
				{
					Snackbar.Add("Cliente no encontrado", Severity.Info);
					await ModalInstance.CloseAsync(ModalResult.Cancel());
					return;
				}
				var responseHttp = await Repository.GetAsync<ActionResponse<Cliente>>($"/api/Cliente/Get/{Id}");
			

				if (responseHttp.Error)
				{					

					var message = await responseHttp.GetErrorMessageAsync();
					await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
					StateHasChanged();
					return;
				}
				_cliente = responseHttp.Response!.Result!;

				_ClienteDTO.NumeroDocumento = _cliente.NumeroDocumento;
				_ClienteDTO.IdCliente = _cliente.IdCliente;
				_ClienteDTO.PrimerNombre = _cliente.PrimerNombre;
				_ClienteDTO.SegundoNombre = _cliente.SegundoNombre;
				_ClienteDTO.PrimerApellido = _cliente.PrimerApellido;
				_ClienteDTO.SegundoApellido = _cliente.SegundoApellido;
				_ClienteDTO.DireccionResidencia = _cliente.DireccionResidencia;
				_ClienteDTO.NumeroCelular = _cliente.NumeroCelular;
				_ClienteDTO.FechaNacimiento = _cliente.FechaNacimiento;
				_ClienteDTO.Email = _cliente.Email;
				_ClienteDTO.IdPlanPreferencia = _cliente.IdPlanPreferencia;
				_ClienteDTO.IdTipoDocumento = _cliente.IdTipoDocumento;

				StateHasChanged();
			}
			catch (Exception ex)
			{				
				var logId = Guid.NewGuid().ToString();			
				await SweetAlertService.FireAsync("Error", $"{"Error al consultal el Cliente"} {"Código "} {logId}", SweetAlertIcon.Error);
				return;
			}

			
		}

		private async void UpdateAsync()
		{
			try
			{
				var validationContext = new ValidationContext(_ClienteDTO, serviceProvider: null, items: null);
				var validationResults = new List<ValidationResult>();

				bool isValid = Validator.TryValidateObject(_ClienteDTO, validationContext, validationResults, validateAllProperties: true);
				if (isValid)
				{
					

					var responseHttp = await Repository.PutAsync<ClienteDTO>("/api/Cliente/Update/" + _ClienteDTO.IdCliente.ToString(), _ClienteDTO);
				

					if (responseHttp.Error)
					{
						
						var message = await responseHttp.GetErrorMessageAsync();
						await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
						StateHasChanged();
						return;
					}

					Snackbar.Add("El Cliente se actualizo correctamente", Severity.Info);
					await ModalInstance.CloseAsync(ModalResult.Ok(_ClienteDTO.PrimerNombre));

				}
				else
				{
					foreach (var validationResult in validationResults)
					{
						Snackbar.Add(validationResult.ErrorMessage, Severity.Error);

						return;

					}
				}

			}
			catch (Exception ex)
			{				
				var logId = Guid.NewGuid().ToString();				
				await SweetAlertService.FireAsync("Error", $"{"Error al actualizar el area "} {"Código "} {logId}", SweetAlertIcon.Error);
				return;
			}
			
		}
		private async void CancelarAsynic()
		{
			await ModalInstance.CloseAsync(ModalResult.Cancel());
		}
	}
}