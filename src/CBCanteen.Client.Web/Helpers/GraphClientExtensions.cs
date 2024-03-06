// <copyright file="GraphClientExtensions.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Authentication.WebAssembly.Msal.Models;
using Microsoft.Graph;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using IAccessTokenProvider =
    Microsoft.AspNetCore.Components.WebAssembly.Authentication.IAccessTokenProvider;

/// <summary>
/// The GraphClientExtensions class provides extension methods for IServiceCollection.
/// </summary>
internal static class GraphClientExtensions
{
    /// <summary>
    /// Adds the GraphClient to the service collection.
    /// </summary>
    /// <param name="services">Service collection used to add the Graph client.</param>
    /// <param name="baseUrl">The base URL of the service.</param>
    /// <param name="scopes">The Graph scopes to be requested.</param>
    /// <returns>ServiceCollection containing the Graph client service.</returns>
    public static IServiceCollection AddGraphClient(
            this IServiceCollection services, string? baseUrl, List<string>? scopes)
    {
        if (string.IsNullOrEmpty(baseUrl) || scopes.IsNullOrEmpty())
        {
            return services;
        }

        services.Configure<RemoteAuthenticationOptions<MsalProviderOptions>>(
            options =>
            {
                scopes?.ForEach((scope) =>
                {
                    options.ProviderOptions.DefaultAccessTokenScopes.Add(scope);
                });
            });

        services.AddScoped<IAuthenticationProvider, GraphAuthenticationProvider>();

        services.AddScoped(sp =>
        {
            return new GraphServiceClient(
                new HttpClient(),
                sp.GetRequiredService<IAuthenticationProvider>(),
                baseUrl);
        });

        return services;
    }

    /// <summary>
    /// The GraphAuthenticationProvider class is responsible for authentication with Graph service APIs.
    /// </summary>
    private class GraphAuthenticationProvider : IAuthenticationProvider
    {
        /// <summary>
        /// Configuration for the GraphAuthenticationProvider.
        /// </summary>
        private readonly IConfiguration config;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphAuthenticationProvider"/> class.
        /// </summary>
        /// <param name="tokenProvider">The IAccessTokenProvider for access tokens.</param>
        /// <param name="config">The IConfiguration for the GraphAuthenticationProvider.</param>
        public GraphAuthenticationProvider(
            IAccessTokenProvider tokenProvider,
            IConfiguration config)
        {
            this.TokenProvider = tokenProvider;
            this.config = config;
        }

        /// <summary>
        /// Gets the provider for access tokens.
        /// </summary>
        public IAccessTokenProvider TokenProvider { get; }

        /// <summary>
        /// Authenticates the request to the Graph API.
        /// </summary>
        /// <param name="request">The request to authenticate.</param>
        /// <param name="additionalAuthenticationContext">Dictionary of additional authentication parameters.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task AuthenticateRequestAsync(
            RequestInformation request,
            Dictionary<string, object>? additionalAuthenticationContext = null,
            CancellationToken cancellationToken = default)
        {
            var result = await this.TokenProvider.RequestAccessToken(
                new AccessTokenRequestOptions()
                {
                    Scopes =
                        this.config.GetSection("MicrosoftGraph:Scopes").Get<string[]>(),
                });

            if (result.TryGetToken(out var token))
            {
                request.Headers.Add(
                    "Authorization",
                    $"{CoreConstants.Headers.Bearer} {token.Value}");
            }
        }
    }
}