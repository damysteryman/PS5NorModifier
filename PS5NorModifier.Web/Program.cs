
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PS5NorModifier.Shared;
using PS5NorModifier.Core;
using MudBlazor.Services;
using Microsoft.AspNetCore.Components.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddScoped<Test>();

await builder.Build().RunAsync();
