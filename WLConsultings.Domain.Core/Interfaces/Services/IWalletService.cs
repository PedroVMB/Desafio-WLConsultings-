using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLConsultings.Domain.Entities;

namespace WLConsultings.Domain.Core.Interfaces.Services
{
    public interface IWalletService
    {
        Task<decimal> GetBalanceAsync(string userId);
        Task DepositAsync(string userId, decimal amount);
        Task TransferAsync(string userId, Guid toWalletId, decimal amount);
        Task<IEnumerable<Transaction>> GetTransactionsAsync(string userId, DateTime? startDate, DateTime? endDate);
    }
}
