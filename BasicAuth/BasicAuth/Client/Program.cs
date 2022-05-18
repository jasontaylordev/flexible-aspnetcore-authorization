using BasicAuth.Client;
using BasicAuth.Client.Authorization;
using BasicAuth.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("BasicAuth.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BasicAuth.ServerAPI"));

builder.Services
    .AddApiAuthorization()
    .AddAccountClaimsPrincipalFactory<CustomAccountClaimsPrincipalFactory>();

builder.Services.Scan(scan => scan
    .FromAssemblyOf<IUsersClient>()
    .AddClasses(classes =>
        classes.InExactNamespaceOf<IUsersClient>())
    .AsImplementedInterfaces()
    .WithScopedLifetime());

await builder.Build().RunAsync();
