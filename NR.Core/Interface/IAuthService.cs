using NR.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NR.Core.Interface
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(string username, string password);
        Task<Admin> GetUserAsync(string username);
    }
}
