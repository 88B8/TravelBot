using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TravelBot.Api.Client.Client;
using TravelBot.Api.Client.Services;
using TravelBot.Web;
using TravelBot.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<IClientAuthService, ClientAuthService>();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthStateProvider>());

builder.Services.AddScoped(_ =>
    new HttpClient
    {
        BaseAddress = new Uri("https://localhost:7288/")
    });

builder.Services.AddScoped<ITravelBotApiClient>(sp =>
{
    var httpClient = sp.GetRequiredService<HttpClient>();
    return new TravelBotApiClient("https://localhost:7288/", httpClient);
});

await builder.Build().RunAsync();