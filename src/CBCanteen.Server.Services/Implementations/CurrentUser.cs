// <copyright file="CurrentUser.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Server.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Web;

namespace CBCanteen.Server.Services.Implementations;

/// <summary>
/// A class that represents the current user.
/// </summary>
internal class CurrentUser : ICurrentUser
{

    /// <summary>
    /// Initializes a new instance of the <see cref="CurrentUser"/> class.
    /// </summary>
    /// <param name="httpContextAccessor">Http context accessor.</param>
    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        this.UserId = httpContextAccessor?
            .HttpContext?
            .User
            .Claims
            .FirstOrDefault(c => c.Type == ClaimConstants.ObjectId)?
            .Value!;
    }

    /// <summary>
    /// Gets the id of the user.
    /// </summary>
    public string UserId { get; }
}
