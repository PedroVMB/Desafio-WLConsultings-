using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLConsultings.Domain.Entities;

namespace WLConsultings.Domain.Core.Interfaces.Repositorys
{
    public interface ITransactionRepository
    {
        Task AddAsync(Transaction transaction);
        Task<IEnumerable<Transaction>> GetByFromWalletIdAsync(Guid walletId, DateTime? startDate, DateTime? endDate);
    }
}
