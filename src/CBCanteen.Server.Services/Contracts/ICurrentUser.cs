using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBCanteen.Server.Services.Contracts;

/// <summary>
/// Provides an interface for the current user.
/// </summary>
public interface ICurrentUser
{
    /// <summary>
    /// Gets the ID of the current user.
    /// </summary>
    public string UserId { get; }

    /// <summary>
    /// Gets the email of the current user.
    /// </summary>
    public string UserEmail { get; }
}
