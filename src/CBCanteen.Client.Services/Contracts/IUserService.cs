using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBCanteen.Client.Services.Contracts;

public interface IUserService
{
    Task<User?> GetCurrentUserInfoAsync();
}
