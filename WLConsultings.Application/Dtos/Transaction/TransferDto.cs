using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLConsultings.Application.Dtos.Transaction
{
    public class TransferDto
    {
        public Guid ToWalletId { get; set; }
        public decimal Amount { get; set; }
    }
}
