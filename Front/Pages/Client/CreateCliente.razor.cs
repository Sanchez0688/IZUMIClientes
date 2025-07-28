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
    public partial class CreateCliente
    {
        private ClienteDTO _cliente = new();
        private List<TiposComboDetalle>? tipoDocumento;
        private List<TiposComboDetalle>? planPref;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [CascadingParameter] private BlazoredModalInstance ModalInstance { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await LoadTipoDocAsync();
            await LoadplanAsync();


        }

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



        private async void NewClienteAsync()
        {
            try
            {
                var validationContext = new ValidationContext(_cliente, serviceProvider: null, items: null);
                var validationResults = new List<ValidationResult>();

                bool isValid = Validator.TryValidateObject(_cliente, validationContext, validationResults, validateAllProperties: true);
                if (isValid)
                {
                    var responseHttp = await Repository.PostAsync("/api/Cliente/Create", _cliente);
                    if (responseHttp.Error)
                    {
                        var message = await responseHttp.GetErrorMessageAsync();
                        await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                        StateHasChanged();
                        return;
                    }
                    Snackbar.Add("El Cliente se creo correctamente", Severity.Success);
                    await ModalInstance.CloseAsync(ModalResult.Ok(_cliente.PrimerNombre));
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
                await SweetAlertService.FireAsync("Error", $"{"Error al registra el Cliente "} {"Codigo"} {logId}", SweetAlertIcon.Error);
                return;
            }
        }

        private async void CancelarAsynic()
        {
            await ModalInstance.CloseAsync(ModalResult.Cancel());
        }

    }
}