// <copyright file="Program.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using Blazored.Modal;
using CBCanteen.Client.Services;
using CBCanteen.Client.Web;
using CBCanteen.Client.Web.Models;
using CBCanteen.Client.Web.UserFactory;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Plk.Blazor.DragDrop;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("CBCanteen.Client.Web.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("CBCanteen.Client.Web.ServerAPI"));

builder.Services
    .AddMsalAuthentication<RemoteAuthenticationState, CustomUserAccount>(
        options =>
        {
            // Bind the "AzureAd" section of appsettings.json file to options.ProviderOptions.Authentication property
            builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);

            // Add the default access token scope
            options.ProviderOptions.DefaultAccessTokenScopes.Add("api://a503036b-8481-4108-ac8a-5219c254f913/API.Access");

            // Set authentication flow to "redirect"
            options.ProviderOptions.LoginMode = "redirect";

            // Set role claim type to "role"
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
