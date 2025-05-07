using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLConsultings.Application.Dtos.Transaction
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public Guid FromWalletId { get; set; }
        public Guid ToWalletId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
