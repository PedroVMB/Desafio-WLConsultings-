using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Management.Smo;
using System.Security.Claims;
using WLConsultings.Application.Dtos.Transaction;
using WLConsultings.Domain.Core.Interfaces.Services;

namespace WLConsultings.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public TransactionController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var transactions = await _walletService.GetTransactionsAsync(userId, startDate, endDate);

            var result = transactions.Select(t => new TransactionDto
            {
                Id = t.Id,
                FromWalletId = t.FromWalletId,
                ToWalletId = t.ToWalletId,
                Amount = t.Amount,
                CreatedAt = t.CreatedAt
            });

            return Ok(result);
        }
    }
}
