// <copyright file="CustomUserAccount.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace CBCanteen.Client.Web.UserFactory;

/// <summary>
/// Represents the custom user account for canteen .
/// </summary>
public class CustomUserAccount : RemoteUserAccount
{
    /// <summary>
    /// Gets or sets the array of groups the user belongs to.
    /// </summary>
    [JsonPropertyName("groups")]
    public string[] Groups { get; set; }

    /// <summary>
    /// Gets or sets the array of roles of the user.
    /// </summary>
    [JsonPropertyName("roles")]
    public string[] Roles { get; set; }
}
