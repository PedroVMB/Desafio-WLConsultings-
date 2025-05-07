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
    public class WalletRepository : IWalletRepository
    {
        private readonly ApplicationDbContext _context;

        public WalletRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Wallet?> GetByUserIdAsync(string userId)
        {
            return await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
        }

        public async Task<Wallet?> GetByIdAsync(Guid id)
        {
            return await _context.Wallets.FindAsync(id);
        }

        public async Task AddAsync(Wallet wallet)
        {
            await _context.Wallets.AddAsync(wallet);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
            await _context.SaveChangesAsync();
        }
    }
}
