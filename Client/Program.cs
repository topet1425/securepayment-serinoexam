using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SecurePayment_SerinoExam.Client;
using SecurePayment.Client.Services;
using SecurePayment.Client.Services.Auth;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("SecurePayment_SerinoExam.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("SecurePayment_SerinoExam.ServerAPI"));
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<PaymentsApiClient>();
builder.Services.AddMudServices();
await builder.Build().RunAsync();
