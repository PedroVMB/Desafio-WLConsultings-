using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLConsultings.Domain.Core.Interfaces.Repositorys;
using WLConsultings.Domain.Entities;

namespace WLConsultings.Infrastructure.Data.Repositorys
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transaction>> GetByFromWalletIdAsync(Guid walletId, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Transactions
                .Where(t => t.FromWalletId == walletId);

            if (startDate.HasValue)
                query = query.Where(t => t.CreatedAt >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(t => t.CreatedAt <= endDate.Value);

            return await query.ToListAsync();
        }
    }
}
