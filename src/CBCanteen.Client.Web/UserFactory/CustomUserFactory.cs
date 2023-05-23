// <copyright file="CustomUserFactory.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using System.Security.Claims;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;

namespace CBCanteen.Client.Web.UserFactory;

/// <summary>
/// Provides custom claims for the user account.<br/>
/// Inherits from <see cref="AccountClaimsPrincipalFactory{TAccount}"/>.
/// </summary>
public class CustomUserFactory : AccountClaimsPrincipalFactory<CustomUserAccount>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomUserFactory"/> class.
    /// </summary>
    /// <param name="accessor">Accessor for accessing access tokens.</param>
    public CustomUserFactory(IAccessTokenProviderAccessor accessor)
        : base(accessor)
    {
    }

    /// <summary>
    /// Creates a claims principal with the provided account.<br/>
    /// Overrides <see cref="AccountClaimsPrincipalFactory{TAccount}.CreateUserAsync(TAccount, RemoteAuthenticationUserOptions)"/>.
    /// </summary>
    /// <param name="account">User account.</param>
    /// <param name="options">Remote authentication user options.</param>
    /// <returns>User claims principal.</returns>
    public async override ValueTask<ClaimsPrincipal> CreateUserAsync(CustomUserAccount account, RemoteAuthenticationUserOptions options)
    {
        var initialUser = await base.CreateUserAsync(account, options);

        if (initialUser.Identity.IsAuthenticated)
        {
            var userIdentity = (ClaimsIdentity)initialUser.Identity;
            if (account?.Roles?.Length > 0)
            {
                foreach (var role in account?.Roles)
                {
                    userIdentity.AddClaim(new Claim("role", role));
                }
            }

            if (account?.Groups?.Length > 0)
            {
                foreach (var group in account?.Groups)
                {
                    userIdentity.AddClaim(new Claim("group", group));
                }
            }
        }

        return initialUser;
    }
}
