using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using WLConsultings.Domain.Core.Interfaces.Repositorys;
using WLConsultings.Domain.Core.Interfaces.Services;

namespace WLConsultings.Domain.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepo;
        private readonly ITransactionRepository _transactionRepo;

        public WalletService(IWalletRepository walletRepo, ITransactionRepository transactionRepo)
        {
            _walletRepo = walletRepo;
            _transactionRepo = transactionRepo;
        }

        public async Task DepositAsync(string userId, decimal amount)
        {
            var wallet = await _walletRepo.GetByUserIdAsync(userId)
             ?? throw new InvalidOperationException("Carteira não encontrada.");

            wallet.Deposit(amount);
            await _walletRepo.UpdateAsync(wallet);
        }

        public async Task<decimal> GetBalanceAsync(string userId)
        {
            var wallet = await _walletRepo.GetByUserIdAsync(userId) ?? throw new InvalidOperationException("Carteira não encontrada.");

            return wallet.Balance;
        }

        public async Task<IEnumerable<Entities.Transaction>> GetTransactionsAsync(string userId, DateTime? startDate, DateTime? endDate)
        {
            var wallet = await _walletRepo.GetByUserIdAsync(userId)
            ?? throw new InvalidOperationException("Carteira não encontrada.");

            return await _transactionRepo.GetByFromWalletIdAsync(wallet.Id, startDate, endDate);
        }

        public async Task TransferAsync(string userId, Guid toWalletId, decimal amount)
        {
            var fromWallet = await _walletRepo.GetByUserIdAsync(userId)
            ?? throw new InvalidOperationException("Carteira de origem não encontrada.");

            var toWallet = await _walletRepo.GetByIdAsync(toWalletId)
                ?? throw new InvalidOperationException("Carteira de destino não encontrada.");

            fromWallet.Withdraw(amount);
            toWallet.Deposit(amount);

            var transaction = new Entities.Transaction(fromWallet.Id, toWallet.Id, amount);

            await _walletRepo.UpdateAsync(fromWallet);
            await _walletRepo.UpdateAsync(toWallet);
            await _transactionRepo.AddAsync(transaction);
        }
    }
}
