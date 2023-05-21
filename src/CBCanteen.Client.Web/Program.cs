using CBCanteen.Client.Web;
using CBCanteen.Client.Services;
using CBCanteen.Client.Web.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.Modal;
using Syncfusion.Blazor;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using CBCanteen.Client.Web.UserFactory;
using Plk.Blazor.DragDrop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("CBCanteen.Client.Web.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("CBCanteen.Client.Web.ServerAPI"));

builder.Services
    .AddMsalAuthentication<RemoteAuthenticationState, CustomUserAccount>(options =>
    {
        builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
        options.ProviderOptions.DefaultAccessTokenScopes.Add("api://a503036b-8481-4108-ac8a-5219c254f913/API.Access");
        options.ProviderOptions.LoginMode = "redirect";
        options.UserOptions.RoleClaim = "role";
    })
    .AddAccountClaimsPrincipalFactory<RemoteAuthenticationState, CustomUserAccount, CustomUserFactory>();

var baseUrl = builder.Configuration.GetSection("MicrosoftGraph")["BaseUrl"];
var scopes = builder.Configuration.GetSection("MicrosoftGraph:Scopes")
    .Get<List<string>>();

builder.Services.AddGraphClient(baseUrl, scopes);
builder.Services.AddServices();
builder.Services.AddBlazoredModal();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddBlazorDragDrop();

await builder.Build().RunAsync();
