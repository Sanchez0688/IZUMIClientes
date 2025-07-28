using Blazored.Modal;
using Front;
using Front.Repositories;
using Front.Settings;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddBlazoredModal(); // Asegúrate de que esta línea esté presente
builder.Services.AddSweetAlert2();

builder.Services.AddSingleton<InformacionService>();
builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7220/") });

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<IRepository, Repository>();
await builder.Build().RunAsync();
