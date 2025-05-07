using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLConsultings.Domain.Entities;

namespace WLConsultings.Domain.Core.Interfaces.Repositorys
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<ApplicationUser> GetByIdAsync(Guid id);
        Task AddAsync(ApplicationUser user);
    }

}
