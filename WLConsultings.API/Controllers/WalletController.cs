using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Management.Smo;
using System.Security.Claims;
using WLConsultings.Application.Dtos.Transaction;
using WLConsultings.Application.Dtos.Wallet;
using WLConsultings.Domain.Core.Interfaces.Services;

namespace WLConsultings.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpGet("balance")]
        public async Task<ActionResult<WalletBalanceDto>> GetBalance()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var balance = await _walletService.GetBalanceAsync(userId);
            return Ok(new WalletBalanceDto { Balance = balance });
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] DepositDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await _walletService.DepositAsync(userId, dto.Amount);
            return NoContent();
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await _walletService.TransferAsync(userId, dto.ToWalletId, dto.Amount);
            return NoContent();
        }
    }
}
