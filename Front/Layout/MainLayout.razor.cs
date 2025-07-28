using Blazored.Modal.Services;
using Front.Repositories;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Front.Layout
{
    public partial class MainLayout
    {
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
     
        [CascadingParameter] IModalService Modal { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    }
}