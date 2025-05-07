using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLConsultings.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(string email, string password);
        Task RegisterAsync(string name, string email, string password);
    }
}
