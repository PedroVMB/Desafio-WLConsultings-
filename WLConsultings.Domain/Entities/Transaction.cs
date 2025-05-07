using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLConsultings.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; private set; }
        public Guid FromWalletId { get; private set; }
        public Guid ToWalletId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Transaction(Guid fromWalletId, Guid toWalletId, decimal amount)
        {
            Id = Guid.NewGuid();
            FromWalletId = fromWalletId;
            ToWalletId = toWalletId;
            Amount = amount;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
