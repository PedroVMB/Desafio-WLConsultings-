using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLConsultings.Domain.Entities;

namespace WLConsultings.Domain.Core.Interfaces.Repositorys
{
    public interface IWalletRepository
    {
        Task<Wallet?> GetByUserIdAsync(string userId);
        Task<Wallet?> GetByIdAsync(Guid id);
        Task AddAsync(Wallet wallet);
        Task UpdateAsync(Wallet wallet);
    }
}
