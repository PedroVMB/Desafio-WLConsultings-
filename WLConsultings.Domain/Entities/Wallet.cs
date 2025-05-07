using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLConsultings.Domain.Entities
{
    public class Wallet
    {
        public Guid Id { get; private set; }
        public string UserId { get; private set; }
        public decimal Balance { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Wallet(string userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Balance = 0m;
            CreatedAt = DateTime.UtcNow;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Valor inválido.");
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Valor inválido.");
            if (Balance < amount) throw new InvalidOperationException("Saldo insuficiente.");
            Balance -= amount;
        }
    }
}
